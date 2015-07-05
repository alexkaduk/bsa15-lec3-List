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
            //    var result2 = from item in list
            //                  where item.Group == "BSA Net"
            //                  select item;
            //var result = users.Where(item => new { Name = item.Name });
            //var result = users.Where(item => item.CategoryId == 1).Select(student => new { Name = student.Name });
            //var result3 = list.Where(item => item.Group == "BSA Net").Select(student => new { Name = student.Name, Mark = student.Mark });
            //var result1 = user. .Where(item => item.Group == "BSA Net");

            foreach (var item in users)
            {
                Console.WriteLine("{0} pass test '{1}' (result mark {2}, pass mark {3})", item.UserName, item.TestName, item.ResultMark, item.PassMark);
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

            foreach (var item in users)
            {
                Console.WriteLine("{0} pass test '{1}' with time {4} (result mark {2}, pass mark {3}, pass time {5})", item.UserName, item.TestName, item.ResultMark, item.PassMark, item.ExecutionTime, item.MaxTestTime);
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

            foreach (var item in users)
            {
                Console.WriteLine("{0} pass test '{1}' with bad time {4} (result mark {2}, pass mark {3}, pass time {5})", item.UserName, item.TestName, item.ResultMark, item.PassMark, item.ExecutionTime, item.MaxTestTime);
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

            foreach (var item in usersFromCity)
            {
                Console.WriteLine("from {0}: {1} student(s)", item.City, item.Count);
            }
        }

        //-	Результат для каждого студента - его баллы, время, баллы в процентах для каждой категории.
        public void UsersResults()
        {
            var usersFromCity = from user in users
                                join work in testWorks on user.Name equals work.UserName
                                join test in tests on work.TestName equals test.TestName
                                select new
                                {
                                    user.Name,
                                    work.ResultMark,
                                    work.ExecutionTime,
                                    test.PassMark,
                                    ResultPercent = work.ResultMark / test.PassMark * 100,
                                };

            foreach (var item in usersFromCity)
            {
                Console.WriteLine("{0}: mark {1}, time {2}, persent {3}% (max {4})", item.Name, item.ResultMark, item.ExecutionTime, item.ResultPercent, item.PassMark);
            }
        }   


        //DataInit();

        //var result1 = user. .Where(item => item.Group == "BSA Net");
        //    var list = new List<Student>
        //    {
        //        new Student
        //        {
        //            Name = "Bill",
        //            Group = "BSA Net",
        //            Mark = 5
        //        },
        //        new Student
        //        {
        //            Name = "Mick",
        //            Group = "BSA Net",
        //            Mark = 4
        //        },
        //        new Student
        //        {
        //            Name = "Dima",
        //            Group = "BSA JS",
        //            Mark = 5
        //        }
        //    };

        //    var result1 = list.Where(item => item.Group == "BSA Net");

        //    //var result1 = list.Where(item => item.Group == "BSA Net").ToList();

        //    var result2 = from item in list
        //                  where item.Group == "BSA Net"
        //                  select item;

        //    var result3 = list.Where(item => item.Group == "BSA Net").Select(student => new { Name = student.Name, Mark = student.Mark });

        //    foreach (var item in result3)
        //    {
        //        Console.WriteLine(item);
        //    }

        //    var someString = "one two three";
        //    var splitted = someString.Split(' ');
        //    var result4 = splitted.Aggregate((revert, item) => item + " " + revert).ToUpper();

        //    Console.WriteLine(result4);

        //    int[] intArray = { 1, 2, 3 };
        //    var max = intArray.Aggregate((sqrSum, item) => sqrSum + item * item);
        //    var max1 = intArray.Max();
        //    Console.WriteLine(max);

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
                    Questions = new int[] { 1, 2 },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #2",
                    CategoryId = 2,
                    Questions = new int[] { 3, 4},
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #3",
                    CategoryId = 3,
                    Questions = new int[] { 5, 6 },
                    MaxTestTime = new TimeSpan(0, 24, 0),
                    PassMark = 4
                },
                new Test
                {
                    TestName = "Test #4",
                    CategoryId = 4,
                    Questions = new int[] { 7, 8 },
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
                    Text = "1 question category 1"
                },
                new Question
                {
                    QuestionId = 2,
                    CategoryId = 1,
                    Text = "2 question category 1"
                },
                new Question
                {
                    QuestionId = 3,
                    CategoryId = 2,
                    Text = "3 question category 1"
                },
                new Question
                {
                    QuestionId = 4,
                    CategoryId = 2,
                    Text = "4 question category 2"
                },
                new Question
                {
                    QuestionId = 5,
                    CategoryId = 3,
                    Text = "5 question category 3"
                },
                new Question
                {
                    QuestionId = 6,
                    CategoryId = 3,
                    Text = "6 question category 3"
                },
                new Question
                {
                    QuestionId = 7,
                    CategoryId = 4,
                    Text = "7 question category 4"
                },
                new Question
                {
                    QuestionId = 8,
                    CategoryId = 4,
                    Text = "8 question category 4"
                },
                new Question
                {
                    QuestionId = 9,
                    CategoryId = 5,
                    Text = "9 question category 5"
                },
                new Question
                {
                    QuestionId = 10,
                    CategoryId = 5,
                    Text = "10 question category 5"
                },
                new Question
                {
                    QuestionId = 11,
                    CategoryId = 6,
                    Text = "11 question category 6"
                },
                new Question
                {
                    QuestionId = 12,
                    CategoryId = 6,
                    Text = "12 question category 6"
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
