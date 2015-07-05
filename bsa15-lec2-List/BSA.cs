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
                },
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
