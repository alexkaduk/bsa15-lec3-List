using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsa15_lec3_List
{
    class BSA
    {
        public BSA()
        {
            DataInit();
        }

        List<User> users;
        List<TestWork> testWorks;
        List<Test> tests;
        List<Question> questions;
        List<Category> categories;

        // -	Список людей, которые прошли тесты.
        public void UsersPassTest()
        {
            var users = from work in testWorks
                        join test in tests on work.TestName equals test.TestName
                        where work.ResultMark >= test.PassMark
                        select new
                        {
                            work.UserName,
                            work.ResultMark,
                            test.TestName,
                            test.PassMark
                        };
            Console.WriteLine("Task #1");
            foreach (var item in users)
            {
                Console.WriteLine("{0} pass '{1}' (mark {2}, pass mark {3})", item.UserName, item.TestName, item.ResultMark, item.PassMark);
            }
        }

        //-	Список тех, кто прошли тесты успешно и уложилися во время.
        public void UsersPassTestWithGoodTime()
        {
            var users = from work in testWorks
                        join test in tests on work.TestName equals test.TestName
                        where (work.ResultMark >= test.PassMark) && (work.ExecutionTime <= test.MaxTestTime)
                        select new
                        {
                            work.UserName,
                            work.ResultMark,
                            work.ExecutionTime,
                            test.TestName,
                            test.PassMark,
                            test.MaxTestTime
                        };

            Console.WriteLine("Task #2");
            foreach (var item in users)
            {
                Console.WriteLine("{0} pass '{1}' with time {4} min (mark {2}, pass mark {3}, pass time {5} min)", item.UserName, item.TestName, item.ResultMark, item.PassMark, item.ExecutionTime.Minutes, item.MaxTestTime.Minutes);
            }
        }

        //-	Список людей, которые прошли тесты успешно и не уложились во время
        public void UsersPassTestWithBadTime()
        {
            var users = from work in testWorks
                        join test in tests on work.TestName equals test.TestName
                        where (work.ResultMark >= test.PassMark) && (work.ExecutionTime > test.MaxTestTime)
                        select new
                        {
                            work.UserName,
                            work.ResultMark,
                            work.ExecutionTime,
                            test.TestName,
                            test.PassMark,
                            test.MaxTestTime
                        };

            Console.WriteLine("Task #3");
            foreach (var item in users)
            {
                Console.WriteLine("{0} pass '{1}' with bad time {4} min (mark {2}, pass mark {3}, pass time {5} min)", item.UserName, item.TestName, item.ResultMark, item.PassMark, item.ExecutionTime.Minutes, item.MaxTestTime.Minutes);
            }
        }

        //-	Список студентов по городам. (Из Львова: 10 студентов, из Киева: 20)
        public void UsersGroupByCity()
        {
            var usersFromCity = from user in users
                                group user by user.City into g
                                select new
                                {
                                    City = g.Key,
                                    Count = g.Count()
                                };

            Console.WriteLine("Task #4");
            foreach (var item in usersFromCity)
            {
                Console.WriteLine("from {0}: {1} student(s)", item.City, item.Count);
            }
        }

        //-	Список успешных студентов по городам.
        public void UsersPassTestGroupByCity()
        {
            var usersFromCity = from user in users
                                join work in testWorks on user.Name equals work.UserName
                                join test in tests on work.TestName equals test.TestName
                                where (work.ResultMark >= test.PassMark) && (work.ExecutionTime <= test.MaxTestTime)
                                group user by user.City into g
                                select new
                                {
                                    City = g.Key,
                                    Count = g.Count()
                                };

            Console.WriteLine("Task #5");
            foreach (var item in usersFromCity)
            {
                Console.WriteLine("from {0}: {1} student(s)", item.City, item.Count);
            }
        }

        //-	Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
        // Кожний тест має у собі кілька питань різноманітних категорій, наприклад, ооп, .нет, бд. 
        // Кожне питання має кількість балів за вірну відповідь, якщо цього не виставляти, то можна рахувати, що вірна відповідь 1 бал. 
        // Таким чином у тесті, наприклад, 5 питань з категорії .нет. Максимально можна набрати 5 балів. Якщо вірно відповів користувач на 3 питання, то це 3 / 5  * 100 = 60%.
        public void UsersResults()
        {
            // виконання до уточнення завдання
            // var usersResults = from user in users
            //                    join work in testWorks on user.Name equals work.UserName
            //                    join test in tests on work.TestName equals test.TestName
            //                    select new
            //                    {
            //                        user.Name,
            //                        work.ResultMark,
            //                        work.ExecutionTime,
            //                        test.PassMark,
            //                        ResultPercent = work.ResultMark / test.PassMark * 100,
            //                    };

            var usersResults = users
                                .Join(testWorks,
                                        user => user.Name,
                                        work => work.UserName,
                                        (user, work) => new { user, work })
                                .Join(tests,
                                        tw => tw.work.TestName,
                                        test => test.TestName,
                                        (tw, test) => new { tw, test })
                                .Select(r => new
                                        {
                                            Name = r.tw.user.Name,
                                            ResultMark = r.tw.work.ResultMark,
                                            ExecutionTime = r.tw.work.ExecutionTime,
                                            TestQuestionMarkSum = r.test.Questions.Sum(q => q.Mark),
                                            ResultPercent = r.tw.work.ResultMark / r.test.Questions.Sum(q => q.Mark) * 100
                                        }
                                );
            
            Console.WriteLine("Task #6");
            Console.WriteLine("*** persent = (student_mark) / (max_mark) * 100 | max_mark = sum(test_question_mark)");
            foreach (var item in usersResults)
            {
                Console.WriteLine("{0}: mark {1}, time {2}, persent {3}% (max {4})", item.Name, item.ResultMark, item.ExecutionTime, item.ResultPercent, item.TestQuestionMarkSum);
            }
        }

        private void DataInit()
        {
            InitCategory();
            InitQuestion();
            InitTest();
            InitTestWork();
            InitUser();
        }

        private void InitUser()
        {
            users = new List<User>
            {
                new User
                {
                    Name = "Mike",
                    Email = "mike@mail.ua",
                    Age = 25,
                    City = "Kyiv",
                    University = "KPI",
                    CategoryId = 1
                },
                new User
                {
                    Name = "John",
                    Email = "john@mail.ua",
                    Age = 27,
                    City = "Kyiv",
                    University = "NaUKMA",
                    CategoryId = 1
                },
                new User
                {
                    Name = "Alex",
                    Email = "alex@mail.ua",
                    Age = 20,
                    City = "Lviv",
                    University = "Lviv Politechnic",
                    CategoryId = 2
                },
                new User
                {
                    Name = "Nick",
                    Email = "nick@mail.ua",
                    Age = 30,
                    City = "Odesa",
                    University = "OPU",
                    CategoryId = 3
                },
                new User
                {
                    Name = "Peter",
                    Email = "peter@mail.ua",
                    Age = 22,
                    City = "Vinnytia",
                    University = "VNTU",
                    CategoryId = 1
                }
            };
        }

        private void InitTestWork()
        {
            testWorks = new List<TestWork>
            {
                new TestWork
                {
                    TestName = "Test #1",
                    UserName = "Mike",
                    ResultMark = 5,
                    ExecutionTime = new TimeSpan(0, 25, 0)
                },
                new TestWork
                {
                    TestName = "Test #2",
                    UserName = "Alex",
                    ResultMark = 4,
                    ExecutionTime = new TimeSpan(0, 24, 0)
                },
                new TestWork
                {
                    TestName = "Test #3",
                    UserName = "Nick",
                    ResultMark = 3,
                    ExecutionTime = new TimeSpan(0, 23, 0)
                },
                new TestWork
                {
                    TestName = "Test #4",
                    UserName = "Peter",
                    ResultMark = 4,
                    ExecutionTime = new TimeSpan(0, 22, 0)
                },
                new TestWork
                {
                    TestName = "Test #1",
                    UserName = "John",
                    ResultMark = 5,
                    ExecutionTime = new TimeSpan(0, 21, 0)
                }
            };
        }

        private void InitTest()
        {
            tests = new List<Test>
            {
                new Test
                {
                    TestName = "Test #1",
                    CategoryId = 1,
                    Questions = new List<Question> 
                                    { 
                                        questions.Single(i => i.QuestionId==1),
                                        questions.Single(i => i.QuestionId==2),
                                    },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #2",
                    CategoryId = 2,
                     Questions = new List<Question> 
                                    { 
                                        questions.Single(i => i.QuestionId==3),
                                        questions.Single(i => i.QuestionId==4),
                                    },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #3",
                    CategoryId = 3,
                     Questions = new List<Question> 
                                    { 
                                        questions.Single(i => i.QuestionId==5),
                                        questions.Single(i => i.QuestionId==6),
                                    },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #4",
                    CategoryId = 4,
                     Questions = new List<Question> 
                                    { 
                                        questions.Single(i => i.QuestionId==7),
                                        questions.Single(i => i.QuestionId==8),
                                    },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                }
            };
        }

        private void InitQuestion()
        {
            questions = new List<Question>
            {
                new Question
                {
                    QuestionId = 1,
                    CategoryId = 1,
                    Text = "1 question category 1",
                    Mark = 3
                },
                new Question
                {
                    QuestionId = 2,
                    CategoryId = 1,
                    Text = "2 question category 1",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 3,
                    CategoryId = 2,
                    Text = "3 question category 1",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 4,
                    CategoryId = 2,
                    Text = "4 question category 2",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 5,
                    CategoryId = 3,
                    Text = "5 question category 3",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 6,
                    CategoryId = 3,
                    Text = "6 question category 3",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 7,
                    CategoryId = 4,
                    Text = "7 question category 4",
                    Mark = 3
                },
                new Question
                {
                    QuestionId = 8,
                    CategoryId = 4,
                    Text = "8 question category 4",
                    Mark = 3
                },
                new Question
                {
                    QuestionId = 9,
                    CategoryId = 5,
                    Text = "9 question category 5",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 10,
                    CategoryId = 5,
                    Text = "10 question category 5",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 11,
                    CategoryId = 6,
                    Text = "11 question category 6",
                    Mark = 2
                },
                new Question
                {
                    QuestionId = 12,
                    CategoryId = 6,
                    Text = "12 question category 6",
                    Mark = 2
                }
            };
        }

        private void InitCategory()
        {
            categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = ".Net"
                },
                new Category 
                {
                    Id = 2,
                    Name = "JS"
                },
                new Category
                {
                    Id = 3,
                    Name = "PHP"
                },
                new Category
                {
                    Id = 4,
                    Name = "DB"
                },
                new Category
                {
                    Id = 5,
                    Name = "OOP"
                },
                new Category
                {
                    Id = 6,
                    Name = "English"
                }
            };
        }
    }
}
