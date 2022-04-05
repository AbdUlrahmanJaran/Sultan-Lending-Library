﻿using System;
using System.Collections;
using System.Collections.Generic;



namespace Sultan_Lending_Library
{
    class Program
    {
        public static Library<Book> Library { get; set; }
        public static List<Book> BookBag { get; set; }

        public static void Main(string[] args)
        {
            //makes new library and bookbag instances
            Library = new Library<Book>();
            BookBag = new List<Book>();
            //calls a method to load library with books
            //calls method for user interface
            PhilOurLibrary();
            UserInterFace();
        }

        public static void UserInterFace()
        {
            int userChoice = 0;
            while (userChoice != 6)
            {
                Console.WriteLine();
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. View All Books");
                Console.WriteLine("2. Add a Book");
                Console.WriteLine("3. Borrow a Book");
                Console.WriteLine("4. Return a Book");
                Console.WriteLine("5. View Your Book Bag");
                Console.WriteLine("6. Exit");

                userChoice = Convert.ToInt32(Console.ReadLine());
                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        ViewLibrary();
                        break;
                    case 2:
                        Console.Clear();
                        AddABookToLibrary();
                        break;
                    case 3:
                        Console.Clear();
                        BorrowABook();
                        break;
                    case 4:
                        Console.Clear();
                        ReturnBook();
                        break;
                    case 5:
                        Console.Clear();
                        ViewBookBag();
                        break;
                    default:
                        break;
                }


            }
        }
        /// <summary>
        /// The below method will show all books in the library that are available.
        /// </summary>
        public static void ViewLibrary()
        {
            Console.WriteLine("Here are the books in the library: \n");

            foreach (Book book in Library)
            {
                if (book != null)
                {
                    Console.WriteLine($"{book.Title} by {book.Author.FirstName} {book.Author.LastName}, with {book.NumberOfPages} pages in the genre: {book.Genre}");
                }
            }
        }

        public static void ViewBookBag()
        {
            Console.WriteLine("Here are the books in your book bag:");
            foreach (Book book in BookBag)
            {
                if (book != null)
                {
                    Console.WriteLine($"{book.Title} by {book.Author.FirstName} {book.Author.LastName}, with {book.NumberOfPages} pages in the genre: {book.Genre}");
                }
            }
        }

        public static void AddABookToLibrary()
        {
            Console.WriteLine("Please enter the title of the book:");
            string thisTitle = Console.ReadLine();
            Console.WriteLine("Please enter the first name of the author");
            string thisFirstName = Console.ReadLine();
            Console.WriteLine("Please enter the last name of the author");
            string thisLastName = Console.ReadLine();
            Console.WriteLine("Please enter the number of pages in the book");
            int thisPageNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please choose a number corresponding to a genre out of this list:");

            foreach (string genre in Enum.GetNames(typeof(Genre)))
            {
                Console.WriteLine($"{genre.GetHashCode()}. {genre}");
            }

            string genreString = Console.ReadLine();
            int genreInt = Convert.ToInt32(genreString);
            Genre genreChoice = (Genre)genreInt;

            Book thisBook = new Book(thisTitle, thisFirstName, thisLastName, thisPageNumber, genreChoice);
            Library.Add(thisBook);
            Console.WriteLine("Successfully added your book to the library");
        }

        public static Book BorrowABook()
        {
            Book borrowedBook = default(Book);
            int indexForNumberedList = 1;
            Console.WriteLine("Which book would you like to borrow?");

            foreach (Book book in Library)
            {
                Console.WriteLine($"{indexForNumberedList}. {book.Title}");
                indexForNumberedList++;
            }

            string userChoice = Console.ReadLine();


            foreach (Book book in Library)
            {
                if (userChoice == null)
                {
                    Console.WriteLine("Sorry that book is not available, try again!");
                }
                else if (userChoice == book.Title)
                {
                    BookBag.Add(Library.Remove(book));
                    borrowedBook = book;
                    Console.WriteLine($"{borrowedBook.Title} was added to your book bag!");
                }

            }
            return borrowedBook;
        }

        static void ReturnBook()
        {
            //instantiates a new Dictionary collection with keys that are integers and values that are book instances.
            Dictionary<int, Book> books = new Dictionary<int, Book>();
            Console.WriteLine("Which book would you like to return");
            int counter = 1;
            // this goes over each item in the BookBag Dictionary feeding in the counter as the keys, and items as values. 
            foreach (var item in BookBag)
            {
                books.Add(counter, item);
                Console.WriteLine($"{counter++}. {item.Title} - {item.Author.FirstName} {item.Author.LastName}");

            }

            string response = Console.ReadLine();
            //below parses the incomming string into an integer
            int.TryParse(response, out int selection);
            //This line gets the value that corresponds to the number key the user inputs from the dictionary.
            books.TryGetValue(selection, out Book returnedBook);
            //removes book from bookbag dictionary
            BookBag.Remove(returnedBook);
            //adds book to library collection
            Library.Add(returnedBook);
        }

        public static void PhilOurLibrary()
        {
            Library.Add(new Book("The Lord of the Rings: Trilogy", "J.R.R.", "Tolkien", 1007, Genre.Fantasy));
            Library.Add(new Book("Carrie", "Steven", "King", 199, Genre.Mystery));
            Library.Add(new Book("The Left Hand Of Darkness", "Ursula K.", "LeGuin", 286, Genre.SciFi));
            Library.Add(new Book("Fifty Shades of Grey", "E.L.", "James", 514, Genre.Romance));
            Library.Add(new Book("Experience the Joy of Painting", "Bob", "Ross", 68, Genre.NonFiction));
            Book johnsBook = new Book()
            {
                Title = "john is bald",
                Author = new Author()
                {
                    FirstName = "John",
                    LastName = "Cokos",

                },
                NumberOfPages = 200,
                Genre = Genre.Mystery
            };
            Library.Add(johnsBook);
        }




    }
}
  

