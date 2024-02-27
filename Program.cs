using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the Name of a file:");
        string ? fileName = Console.ReadLine();
        // Append ".csv" if the user input does not already end with ".csv"
        if (!string.IsNullOrEmpty(fileName) && !fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            fileName += ".csv";
        }

        // Checking for the file in the current directory
        if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
        {
            // Regex pattern for validating email
            var emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var validEmails = "Valid Emails:\n";
            var invalidEmails = "Invalid Emails:\n";

            using (var reader = new StreamReader(fileName))
            {
                // Skip the first line (header so firstname, lastname and email address are not printed as invalid)
                reader.ReadLine();

                string ? line;
                while ((line = reader.ReadLine()) != null)
                {
                    
                    var values = line.Split(',');

                    //the email is in the third column and there are exactly three columns
                    if (values.Length == 3 && Regex.IsMatch(values[2], emailPattern))
                    {
                        validEmails += string.Join(" | ", values) + "\n";
                    }
                    else if (values.Length == 3)
                    {
                        invalidEmails += string.Join(" | ", values) + "\n";
                    }
                }
            }

            // Print valid and invalid email data
            Console.WriteLine(validEmails);
            Console.WriteLine(invalidEmails);
        }
        else
        {
            Console.WriteLine("File does not exist");
        }
    }
}

/*
References-:
https://stackoverflow.com/questions/5342375/regex-email-validation
https://codetosolutions.com/blog/74/working-with-csv-files-in-c
*/
