using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FirstAndroidTest.AndroidManifest;

// http://czak.pl/2016/05/31/handbuilt-android-project.html
// http://czak.pl/2016/01/13/minimal-android-project.html
// https://medium.com/@authmane512/how-to-build-an-apk-from-command-line-without-ide-7260e1e22676

// Hyper-V Ubuntu 18.04 server
// https://thishosting.rocks/how-to-enable-ssh-on-ubuntu/
// FRÅN MIN DATOR:
// scp app ato@172.16.31.136:/home/ato/app
// ssh ato@172.16.31.136
// sudo apt-get update
// sudo apt-get install aapt
// sudo apt-get install android-sdk

namespace FirstAndroidTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string androidSdkVersion = "27.0.0";

            string package = "se.atornblad.firstAndroidTest";

            Directory.CreateDirectory($"D:\\privat\\app-maker");
            Environment.CurrentDirectory = "D:\\privat\\app-maker";

            Directory.CreateDirectory($"D:\\privat\\app-maker\\app\\res");
            Directory.CreateDirectory($"D:\\privat\\app-maker\\app\\src\\main");
            Directory.CreateDirectory($"D:\\privat\\app-maker\\app\\src\\main\\{package.Replace('.', '\\')}");

            var page = new SimpleTextPage("main", "Hello, World!");
            var generator = new AndroidActivityCodeGenerator(page, package);
            Console.WriteLine($"Creating src/main/{package.Replace('.', '/')}/MainActivity.java");
            using (var javaOutput = File.CreateText($"D:\\privat\\app-maker\\app\\src\\main\\{package.Replace('.', '\\')}\\MainActivity.java"))
            {
                javaOutput.NewLine = "\n";
                generator.Write(javaOutput);
                javaOutput.Flush();
            }

            Console.WriteLine($"Creating AndroidManifest.xml");
            var manifest = new Manifest();
            manifest.Package = package;
            manifest.Application.Label = "Minimal";
            var mainActivity = new Activity();
            manifest.Application.AddActivity(mainActivity);
            mainActivity.Name = "MainActivity";
            mainActivity.AddIntentFilter(new IntentFilter { Action = IntentAction.MainAction, Category = IntentCategory.LauncherCategory });
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("android", Manifest.AndroidXmlNs);
            var xmlSerializer = new XmlSerializer(typeof(Manifest));
            using (var manifestOutput = File.CreateText($"D:\\privat\\app-maker\\app\\AndroidManifest.xml"))
            {
                manifestOutput.NewLine = "\n";
                xmlSerializer.Serialize(manifestOutput, manifest, ns);
                manifestOutput.Flush();
            }

            Console.WriteLine($"Creating build.sh");
            using (var buildOutput = File.CreateText($"D:\\privat\\app-maker\\app\\build.sh"))
            {
                buildOutput.NewLine = "\n";

                buildOutput.WriteLine("#!/bin/sh");
                buildOutput.WriteLine();
                buildOutput.WriteLine("# 1. Generate R.java (right now does nothing, because there are no resources)");
                buildOutput.WriteLine("mkdir -p ./gen");
                buildOutput.WriteLine("# aapt package -f -M ./AndroidManifest.xml -I \"$ANDROID_HOME/platforms/android-23/android.jar\" -S res/ -J gen/ -m");
                buildOutput.WriteLine();
                buildOutput.WriteLine("# 2. Compile");
                buildOutput.WriteLine("mkdir -p ./obj");
                buildOutput.WriteLine($"javac -source 1.7 -target 1.7 -d obj -classpath src -bootclasspath $ANDROID_HOME/platforms/android-23/android.jar src/main/{package.Replace('.', '/')}/*.java");
                buildOutput.WriteLine();
                buildOutput.WriteLine("# 3. Convert to dex");
                buildOutput.WriteLine("mkdir -p ./bin");
                buildOutput.WriteLine($"$ANDROID_HOME/build-tools/24.0.0/dx --dex --output=./bin/classes.dex ./obj");
                buildOutput.WriteLine();
                buildOutput.WriteLine("# 4. Package to apk");
                buildOutput.WriteLine($"aapt package -f -m - -F ./bin/hello.unaligned.apk -M ./AndroidManifest.xml -S ./res -I $ANDROID_HOME/platforms/android-23/android.jar");
                buildOutput.WriteLine($"cp ./bin/classes.dex ./");
                buildOutput.WriteLine($"aapt add ./bin/hello.unaligned.apk ./classes.dex");

                buildOutput.Flush();
            }
        }
    }
}
