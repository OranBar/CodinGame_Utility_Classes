﻿using System;
using System.Collections.Generic;
using System.IO;


public class Builder
{
    public void Build()
    {
        DirectoryInfo info = Directory.GetParent(Directory.GetCurrentDirectory());
        info = Directory.GetParent(info.FullName);
        FileInfo[] files = info.GetFiles();

        HashSet<string> allUsingStatements = new HashSet<string>();

        using (StreamWriter writer = File.CreateText("GodClass.cs"))
        {
            //Get all using statements
            foreach(FileInfo f in files){
                if (f.Name.Equals("Builder.cs")==false && f.Extension.Equals(".cs")) {
                    using(StreamReader reader = f.OpenText()){
                        Console.WriteLine(f.FullName);
                    
                        while(true){
                            string line = reader.ReadLine();
                            if (line.Contains("using"))
                            {
                                allUsingStatements.Add(line);
                            } else {
                                break;
                            }
                        }
                    }
                }
            }
            //Write all using statements
            foreach(string usingStatement in allUsingStatements){
                writer.WriteLine(usingStatement);
            }

            //Get all classes code
            foreach(FileInfo f in files){
                if (f.Name.Equals("Builder.cs")==false && f.Extension.Equals(".cs")) {
                    using (StreamReader reader = f.OpenText())
                    {
                        Console.WriteLine(f.FullName);
                        string line = "\n";
                        while(true){
                            line = reader.ReadLine();
                            if (line.Contains("using")==false){
                                break;
                            }
                        }
                        //Write classes code
                        writer.WriteLine(line);
                        writer.Write(reader.ReadToEnd());
                    }
                }
            }
        }


        
            
        
    
    }

    public static int Main(string[] args)
    {
        Builder b = new Builder();
        b.Build();
        Console.ReadLine();
        return 0;
    }
}
