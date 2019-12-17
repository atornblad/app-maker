using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FirstAndroidTest.AndroidManifest
{
    [XmlRoot("manifest")]
    [XmlInclude(typeof(Application))]
    [XmlInclude(typeof(Activity))]
    [XmlInclude(typeof(IntentFilter))]
    [XmlInclude(typeof(IntentAction))]
    [XmlInclude(typeof(IntentCategory))]
    public class Manifest
    {
        public const string AndroidXmlNs = "http://schemas.android.com/apk/res/android";

        [XmlAttribute("package")]
        public string Package { get; set; }

        [XmlElement("application")]
        public Application Application { get; set; }

        public Manifest()
        {
            Application = new Application();
        }
    }

    public class Application
    {
        [XmlAttribute("label", Namespace = Manifest.AndroidXmlNs)]
        public string Label { get; set; }

        private List<Activity> activities = new List<Activity>();

        [XmlElement("activity")]
        public Activity[] Activities
        {
            get
            {
                return activities.ToArray();
            }
            set
            {
                activities.Clear();
                activities.AddRange(value);
            }
        }

        public void AddActivity(Activity activity)
        {
            activities.Add(activity);
        }
    }

    public class Activity
    {
        [XmlAttribute("name", Namespace = Manifest.AndroidXmlNs)]
        public string Name { get; set; }

        private List<IntentFilter> intentFilters = new List<IntentFilter>();

        [XmlElement("intent-filter")]
        public IntentFilter[] IntentFilters
        {
            get
            {
                return intentFilters.ToArray();
            }
            set
            {
                intentFilters.Clear();
                intentFilters.AddRange(value);
            }
        }

        public void AddIntentFilter(IntentFilter intentFilter)
        {
            intentFilters.Add(intentFilter);
        }
    }

    public class IntentFilter
    {
        [XmlElement("action")]
        public IntentAction Action { get; set; }
        [XmlElement("category")]
        public IntentCategory Category { get; set; }

        public IntentFilter()
        {
            Action = new IntentAction();
            Category = new IntentCategory();
        }
    }

    public class IntentAction
    {
        [XmlAttribute("name", Namespace = Manifest.AndroidXmlNs)]
        public string Name { get; set; }

        public const string MainActionName = "android.intent.action.MAIN";

        public static implicit operator IntentAction(string name)
        {
            return new IntentAction { Name = name };
        }

        public static IntentAction MainAction { get; } = MainActionName;
    }

    public class IntentCategory
    {
        [XmlAttribute("name", Namespace = Manifest.AndroidXmlNs)]
        public string Name { get; set; }

        public const string LauncherCategoryName = "android.intent.category.LAUNCHER";

        public static implicit operator IntentCategory(string name)
        {
            return new IntentCategory { Name = name };
        }

        public static IntentCategory LauncherCategory { get; } = LauncherCategoryName;
    }
}
