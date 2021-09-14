using SlothBookStore.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace SlothBookStore.Data
{
    public class DbInitializer
    {
        public static void Initialize(BookDbContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.EnsureCreated();

            SetupUsers(userManager).ConfigureAwait(false).GetAwaiter().GetResult();

            Seed(context);
        }

        static void Seed(BookDbContext context)
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = "1",
                    Name = "湯姆歷險記",
                    Author = "馬克•吐溫",
                    Desc = "鬼靈精怪的湯姆，常和好朋友哈克做出許多令人捧腹的妙事。馬克•吐溫以幽默詼諧的筆調，把湯姆頑皮機智的模樣、緊張刺激的冒險歷程，描繪得生動有趣，連大人也愛看！",
                    Price = 349,
                },
                new Book
                {
                    Id = "2",
                    Name = "孤雛淚",
                    Author = "查爾斯·狄更斯",
                    Desc = "孤苦無依的奧利佛，只是想多要一點稀飯就被趕出救濟院，到棺材店當學徒，又受人凌虐。逃離後，被引誘到倫敦當扒手，誤入竊盜集團，雖遇到和藹可親的老紳士，卻被陰狠的凶手綁架。他的人生會失去希望嗎？",
                    Price = 450,
                },
                new Book
                {
                    Id = "3",
                    Name = "小公主",
                    Author = "法蘭西絲·霍森·柏納特",
                    Desc = "原本集萬千寵愛於一身的「小公主」莎拉，隨著父親去世，開始過著悲慘艱苦的生活。所幸樂觀善良的莎拉，總是以豐富的想像力，維持公主般的風度，勇敢面對殘酷的現實。後來，是什麼樣的奇蹟，讓她人生恢復光彩呢？",
                    Price = 197,
                },
                new Book
                {
                    Id = "4",
                    Name = "孤女努力記",
                    Author = "赫克特‧馬羅",
                    Desc = "孤苦伶仃的佩玲，遵從母親的遺言，回到父親的故鄉。途中，歷盡了千辛萬苦，她總是擦乾眼淚，勇敢向前。但面對富有卻失明的祖父，她不敢相認。誠實又善良的佩玲，能不能獲得原本屬於她的幸福呢？",
                    Price = 252,
                },
                new Book
                {
                    Id = "5",
                    Name = "地底旅行",
                    Author = "裘爾．維納",
                    Desc = "大膽又好奇的地質學教授白洛克博士，帶著姪子從德國來到冰島，深入火山口，一路驚險的往地心探險，他們發現了地底大海、巨型古代生物、人類與動物骸骨，遭遇種種危險，在九死一生中，他們如何重返地球表面呢？",
                    Price = 434,
                },
                new Book
                {
                    Id = "6",
                    Name = "環遊世界八十天",
                    Author = "儒勒．凡爾納",
                    Desc = "在一次賭注中，神祕而富有的英國紳士霍格，帶著耿直忠心的僕人帕斯巴德，展開分秒必爭的環遊世界之旅。途中，他們透過不同的交通工具，遊歷各國，卻因刑警費克的百般阻擾，最終能否順利在八十天內環繞地球一圈呢？",
                    Price = 450,
                },
            };
            

            foreach(var b in books)
                context.BooksSet.Add(b);

            context.SaveChanges();
        }

        static async Task SetupUsers(UserManager<IdentityUser> userManager)
        {
            var aliceEmail = "user@test.com";

            if (await userManager.FindByEmailAsync(aliceEmail) == null)
            {
                var alice = new IdentityUser
                {
                    UserName = aliceEmail,
                    Email = aliceEmail,
                };

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    //建立 user, 密碼 Aa123456
                    await userManager.CreateAsync(alice, "Aa123456");

                    scope.Complete();
                }
            }
        }
    }
}