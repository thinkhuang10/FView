using System;
using System.Media;
using System.Windows.Forms;
using Alert_Main.Properties;

namespace Alert_Main;

public class PlayAlarmSound
{
	private SoundPlayer mPlayer = new SoundPlayer(Resources.alarm_wav);

	private Timer mTimer = new Timer();

	private int mCounter;

	public PlayAlarmSound()
	{
		mTimer.Interval = 1000;
		mTimer.Tick += mTimer_Tick;
	}

	public bool SetSoundLocation(string soundLocation)
	{
		if (string.IsNullOrEmpty(soundLocation))
		{
			mPlayer.Stream = Resources.alarm_wav;
			return true;
		}
		mPlayer.Stream = null;
		mPlayer.SoundLocation = soundLocation;
		try
		{
			mPlayer.Load();
		}
		catch (Exception ex)
		{
			MessageBox.Show("报警音[" + mPlayer.SoundLocation + "]异常: " + ex.Message, "报警音异常");
			mPlayer.Stream = Resources.alarm_wav;
			return false;
		}
		return true;
	}

	private void mTimer_Tick(object sender, EventArgs e)
	{
		mTimer.Stop();
		Stop();
		mCounter--;
		if (mCounter > 0)
		{
			mTimer.Start();
			Play();
		}
	}

	public void Play()
	{
		if (mPlayer == null)
		{
			return;
		}
		try
		{
			mPlayer.PlayLooping();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void PlayWithTimes(int times)
	{
		if (mPlayer != null)
		{
			mCounter = times;
			if (mCounter > 0)
			{
				mTimer.Start();
			}
			Play();
		}
	}

	public void Stop()
	{
		if (mPlayer != null)
		{
			mPlayer.Stop();
		}
	}
}
