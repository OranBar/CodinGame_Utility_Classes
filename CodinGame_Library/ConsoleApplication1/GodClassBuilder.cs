using System;
using System.Collections.Generic;
using System.IO;


public class GodClassBuilder
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
                        string line = "";
                        do
                        {
                            line = reader.ReadLine();
                        } while (line.Contains("using"));

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
        GodClassBuilder b = new GodClassBuilder();
        b.Build();
        return 0;
    }
}

