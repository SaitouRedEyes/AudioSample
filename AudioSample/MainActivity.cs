using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;
using Java.Lang;
using Android.Util;

namespace AudioSample
{
    [Activity(Label = "AudioSample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private MediaPlayer mpRaw;
        private MediaPlayer mpPath;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mpRaw = MediaPlayer.Create(this, Resource.Raw.intro);
            mpPath = new MediaPlayer();

            Button buttonPlayRaw = (Button)FindViewById(Resource.Id.buttonPlayRaw);
            Button buttonPlayPath = (Button)FindViewById(Resource.Id.buttonPlayPath);
            Button buttonStop = (Button)FindViewById(Resource.Id.buttonStop);

            buttonPlayRaw.Click += ButtonPlayRaw_Click;
            buttonPlayPath.Click += ButtonPlayPath_Click;
            buttonStop.Click += ButtonStop_Click;
        }

        private void ButtonStop_Click(object sender, System.EventArgs e)
        {
            MPManager(1);
        }

        private void ButtonPlayPath_Click(object sender, System.EventArgs e)
        {
            if(mpPath != null && !mpPath.IsPlaying && !mpRaw.IsPlaying)
            {
                
                try
                {
                    mpPath.Reset();
                    mpPath.SetDataSource("http://soundbible.com/grab.php?id=2207&type=wav");
                    //mpPath.SetDataSource("/sdcard/intro.mp3");
                    mpPath.Prepare();
                    mpPath.Start();
                }
                catch(Exception error)
                {
                    Log.Debug("ERRO", error.Message);
                }
            }
        }

        private void ButtonPlayRaw_Click(object sender, System.EventArgs e)
        {
            if (mpRaw != null && !mpRaw.IsPlaying && !mpPath.IsPlaying)
            {
                mpRaw = MediaPlayer.Create(this, Resource.Raw.intro);
                mpRaw.Start();
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            MPManager(0);
        }

        protected override void OnStop()
        {
            base.OnStop();

            MPManager(1);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            MPManager(2);
        }

        private void MPManager(int state)
        {
            if (mpRaw != null && mpRaw.IsPlaying)
            {
                switch(state)
                {
                    case 0: mpRaw.Pause(); break;
                    case 1: mpRaw.Stop(); break;
                    case 2: mpRaw.Release(); break;
                }
                
            }
            else if (mpPath != null && mpPath.IsPlaying)
            {
                switch (state)
                {
                    case 0: mpPath.Pause(); break;
                    case 1: mpPath.Stop(); break;
                    case 2: mpPath.Release(); break;
                }
            }
        }
    }
}

