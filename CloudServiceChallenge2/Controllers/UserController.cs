using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CloudServiceChallenge2.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace CloudServiceChallenge2.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDBContext _context;

        public UserController(UserDBContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Users.ToListAsync());
        }

        // GET: User/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDataModel = await _context.Users.Include(x => x.Relation).ThenInclude(y => y.Title).SingleOrDefaultAsync(m => m.UserId == id);
            if (userDataModel == null)
            {
                return NotFound();
            }

            return View(userDataModel);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 100データを作成します
        /// </summary>
        public async Task<IActionResult> Create100Data()
        {
            UserDataModel usermodel = new UserDataModel();

            Random ran = new Random();
            String letters = "abcdefghijklmnopqrstuvwxyz";
            int length = 8;
            int maxid = 0;
            String randomName = "";

            for (int j = 0; j < 100; j++)
            {
                randomName = "";
                var rec = _context.Users.FirstOrDefault();

                if (rec == null)
                {
                    maxid = 0;
                }
                else
                {
                    maxid = _context.Users.Max(p => p.UserId);
                }
                for (int i = 0; i < length; i++)
                {
                    int c = ran.Next(26);
                    randomName = randomName + letters.ElementAt(c);
                }

                usermodel.UserId = maxid + 1;
                usermodel.NumberOfWins = 0;
                usermodel.NumberOfDefeats = 0;
                usermodel.NumberOfDraws = 0;
                usermodel.UserName = randomName;

                _context.Add(usermodel);
                _context.Database.OpenConnection();
                try
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users ON");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Users OFF");
                }
                finally
                {
                    _context.Database.CloseConnection();
                }

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// ランダムな対戦相手と1-10のランダムな試合を作成する
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Play()
        {
            PlayerHistoryModel historyModel = new PlayerHistoryModel();
            Random ran = new Random();
            IEnumerable<UserDataModel> userList = _context.Users.ToList();

            int player2Index; var checkHistory1=0; var checkHistory2 = 0;int result = 0; var player2MatchCount = 0;
            foreach (var user in userList)
            {
                // 誰が誰と遊んだかを追跡する
                IEnumerable<PlayerHistoryModel> playerHistory = _context.PlayerHistory.ToList();
                int matchCount = ran.Next(10);
                int id = user.UserId;
                var matchOcurred = CountMatch(user.UserId);

                // マッチカウント
                if (matchCount - matchOcurred > 0)
                {
                    int count = matchCount - matchOcurred;
                    for(int i = 0; i < count; i++)
                    {
                        playerHistory = _context.PlayerHistory.ToList();
                        // プレーヤー2を見つけるのために
                        while (true)
                        {
                            player2Index = ran.Next(1,userList.Count());
                            player2MatchCount = CountMatch(player2Index);
                            checkHistory1 = playerHistory.Where(e => e.Player1Id == user.UserId).Where(e => e.Player2Id == player2Index).Count();
                            checkHistory2 = playerHistory.Where(e => e.Player1Id == player2Index).Where(e => e.Player2Id == user.UserId).Count();

                            if (user.UserId != player2Index && checkHistory1==0 && checkHistory2==0 && player2MatchCount<10)
                            {
                                break;
                            }
                        }
                        result = ran.Next(0, 3);
                        int maxid = 0;
                        var rec = _context.PlayerHistory.FirstOrDefault();
                        // プレイヤー履歴データベースに挿入
                        if (rec == null)
                        {
                            maxid = 0;
                        }
                        else
                        {
                            maxid = _context.PlayerHistory.Max(p => p.Id);
                        }
                        historyModel.Id = maxid + 1;
                        historyModel.Player1Id = user.UserId;
                        historyModel.Player2Id = player2Index;
                        historyModel.Result = result;
                        _context.Add(historyModel);
                        _context.Database.OpenConnection();
                        try
                        {
                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PlayerHistory ON");
                            _context.SaveChanges();
                            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PlayerHistory OFF");
                        }
                        finally
                        {
                            _context.Database.CloseConnection();
                        }
                        // ユーザーデータテーブルの更新
                        if (result == 0) {
                            userList.Where(e => e.UserId == user.UserId).Select(c=> { c.NumberOfDefeats = c.NumberOfDefeats + 1;return c; }).ToList();
                            userList.Where(e => e.UserId == player2Index).Select(c => { c.NumberOfWins = c.NumberOfWins + 1; return c; }).ToList();

                        }
                        else if(result == 1){
                            userList.Where(e => e.UserId == user.UserId).Select(c => { c.NumberOfWins = c.NumberOfWins + 1; return c; }).ToList();
                            userList.Where(e => e.UserId == player2Index).Select(c => { c.NumberOfDefeats = c.NumberOfDefeats + 1; return c; }).ToList();

                        }
                        else {
                            userList.Where(e => e.UserId == user.UserId).Select(c => { c.NumberOfDraws = c.NumberOfDraws + 1; return c; }).ToList();
                            userList.Where(e => e.UserId == player2Index).Select(c => { c.NumberOfDraws = c.NumberOfDraws + 1; return c; }).ToList();


                        }
                        await _context.SaveChangesAsync();

                    }
                    await _context.SaveChangesAsync();
                }
            }
          
            await _context.SaveChangesAsync();
            // 題名のために
            CheckTitle();
            return RedirectToAction("Index");

        }
        /// <summary>
        /// すべてのユーザーデータから勝ち数を-1減らします
        /// </summary>
        public async Task<IActionResult> FixError()
        {
            _context.Database.ExecuteSqlRaw("Update Users Set NumberOfWins=NumberOfWins-1;");
            _context.Database.ExecuteSqlRaw("Update Users Set NumberOfWins=0 Where NumberOfWins<0;");
            _context.Database.ExecuteSqlRaw("truncate Table PossessionTitle;");
            await _context.SaveChangesAsync();
            CheckTitle();
            return RedirectToAction("Index");

        }

        /// <summary>
        /// 勝率で上位10人のユーザーを表示
        /// </summary>
        public ActionResult SortUserData()
        {
            
            IEnumerable<UserDataModel> userList = _context.Users.ToList();

               var result = userList.Where(e=> (e.NumberOfWins + e.NumberOfDefeats + e.NumberOfDraws) != 0).
                OrderByDescending(e =>(e.NumberOfWins/(e.NumberOfWins + e.NumberOfDefeats + e.NumberOfDraws))).ThenBy(e => e.UserName).Take(10);

            return View(result);
        }

        /// <summary>
        /// マッチカウントのために
        /// </summary>
        /// <param name="id"></param>
        /// <returns>マッチカウント</returns>
        public int CountMatch(int id) {
            IEnumerable<PlayerHistoryModel> playerHistory = _context.PlayerHistory.ToList();
            var matchOcurred = playerHistory.Count(e => e.Player1Id == id);
            matchOcurred = matchOcurred + playerHistory.Count(e => e.Player2Id == id);
            return matchOcurred;
        }
        /// <summary>
        ///題名のために勝ち数を確認する
        /// </summary>
        public void CheckTitle() {
            IEnumerable<UserDataModel> userList = _context.Users.ToList();
           
            foreach (var user in userList)
            {
                
                if (user.NumberOfWins >=2) {
                    Insert(user.UserId, 1);
                    if (user.NumberOfWins >= 4)
                    {
                        Insert(user.UserId, 6);
                        if (user.NumberOfWins >= 5)
                        {
                            Insert(user.UserId, 7);
                        }
                    }
                }
                
            }
      
        }
        /// <summary>
        /// タイトルテーブルに挿入
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="titleId"></param>
        public void Insert(int userId, int titleId) {
            PossessionTitleDataModel possessionTitleDataModel = new PossessionTitleDataModel();
            int maxid = 0;
            var rec = _context.PossessionTitle.FirstOrDefault();

            if (rec == null)
            {
                maxid = 0;
            }
            else
            {
                maxid = _context.PossessionTitle.Max(p => p.Id);
            }
            possessionTitleDataModel.Id = maxid + 1;
            possessionTitleDataModel.UserId = userId;
            possessionTitleDataModel.TitleId = titleId;
            _context.Add(possessionTitleDataModel);
            _context.Database.OpenConnection();
            try
            {
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PossessionTitle ON");
                _context.SaveChanges();
                _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.PossessionTitle OFF");
            }
            finally
            {
                _context.Database.CloseConnection();
            }
        }
        /// <summary>
        /// すべてのデータを削除する
        /// </summary>
        public ActionResult DeleteAllData()
        {
            _context.Database.ExecuteSqlRaw("truncate Table PlayerHistory;");
            _context.Database.ExecuteSqlRaw("Delete from Users Where NumberOfWins!=-2;");
            return RedirectToAction("Index");

        }
        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,NumberOfWins,NumberOfDefeats,NumberOfDraws")] UserDataModel userDataModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDataModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userDataModel);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDataModel = await _context.Users.FindAsync(id);
            if (userDataModel == null)
            {
                return NotFound();
            }
            return View(userDataModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,NumberOfWins,NumberOfDefeats,NumberOfDraws")] UserDataModel userDataModel)
        {
            if (id != userDataModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDataModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDataModelExists(userDataModel.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userDataModel);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDataModel = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDataModel == null)
            {
                return NotFound();
            }

            return View(userDataModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userDataModel = await _context.Users.FindAsync(id);
            _context.Users.Remove(userDataModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserDataModelExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
