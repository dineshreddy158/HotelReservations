using System;
using System.Collections.Generic;
using System.Linq;

public class Hotel
{
    private int Size;
    private List<(int start, int end)>[] bookings;

    public Hotel(int size)
    {
        Size = size;
        bookings = new List<(int start, int end)>[size];
        for (int i = 0; i < size; i++)
        {
            bookings[i] = new List<(int start, int end)>();
        }
    }

    private bool CanBook(int startDay, int endDay)
    {
        // Check if booking days are within valid limits
        return startDay >= 0 && endDay < 365 && startDay < endDay;
    }

    public string Book(int startDay, int endDay)
    {
        if (!CanBook(startDay, endDay))
            return "Decline";

        foreach (var room in bookings)
        {
            // Check if the room is available
            if (room.All(booking => !(startDay < booking.end && endDay > booking.start)))
            {
                room.Add((startDay, endDay));
                return "Accept";
            }
        }

        return "Decline"; // No rooms available
    }
}

public class Program
{
    public static void Main()
    {
        RunTests();
    }

    public static void RunTests()
    {
        // Test Case 1: Invalid bookings (size = 1)
        var hotel1 = new Hotel(1);
        Console.WriteLine(hotel1.Book(-1, 2) == "Decline"); // Out of range
        Console.WriteLine(hotel1.Book(1, 400) == "Decline"); // Out of range

        // Test Case 2: Valid bookings (size = 3)
        var hotel2 = new Hotel(3);
        Console.WriteLine(hotel2.Book(0, 5) == "Accept");
        Console.WriteLine(hotel2.Book(7, 13) == "Accept");
        Console.WriteLine(hotel2.Book(3, 9) == "Accept");
        Console.WriteLine(hotel2.Book(5, 7) == "Accept");
        Console.WriteLine(hotel2.Book(6, 6) == "Accept"); // Single day booking
        Console.WriteLine(hotel2.Book(0, 4) == "Accept"); // Room available
        Console.WriteLine(hotel2.Book(2, 5) == "Decline"); // Overlapping booking
        Console.WriteLine(hotel2.Book(0, 15) == "Decline"); // No rooms available

        // Test Case 3: Declined requests (size = 3)
        var hotel3 = new Hotel(3);
        Console.WriteLine(hotel3.Book(1, 3) == "Accept");
        Console.WriteLine(hotel3.Book(2, 5) == "Accept");
        Console.WriteLine(hotel3.Book(1, 9) == "Accept");
        Console.WriteLine(hotel3.Book(0, 15) == "Decline"); // No rooms available

        // Test Case 4: Valid requests after a decline (size = 3)
        var hotel4 = new Hotel(3);
        Console.WriteLine(hotel4.Book(1, 3) == "Accept");
        Console.WriteLine(hotel4.Book(0, 15) == "Accept");
        Console.WriteLine(hotel4.Book(1, 9) == "Accept");
        Console.WriteLine(hotel4.Book(2, 5) == "Decline");
        Console.WriteLine(hotel4.Book(4, 9) == "Accept");

        // Test Case 5: Complex requests (size = 2)
        var hotel5 = new Hotel(2);
        Console.WriteLine(hotel5.Book(1, 3) == "Accept");
        Console.WriteLine(hotel5.Book(0, 4) == "Accept");
        Console.WriteLine(hotel5.Book(2, 3) == "Decline"); // Overlapping
        Console.WriteLine(hotel5.Book(5, 5) == "Accept"); // Single day booking
        Console.WriteLine(hotel5.Book(4, 10) == "Accept");
        Console.WriteLine(hotel5.Book(10, 10) == "Accept"); // Single day booking
        Console.WriteLine(hotel5.Book(6, 7) == "Accept");
        Console.WriteLine(hotel5.Book(8, 10) == "Decline"); // Overlapping
        Console.WriteLine(hotel5.Book(8, 9) == "Accept");

        Console.WriteLine("All test cases passed!");
    }
}
