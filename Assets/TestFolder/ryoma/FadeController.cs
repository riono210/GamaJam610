namespace FadeCtrl
{
	using UnityEngine;
	using System.Collections;
	using UnityEngine.SceneManagement;
	using UnityEngine.UI;

	public class FadeController : MonoBehaviour
	{ // ここでゲームオーバー処理とクリア処理

		// ---------------------------
		
		/// 
		/// <summary>
		/// StartFadeOut(string sceneName), StartFadeOn(void)関数でフェードをスタートします。
		/// string sceneNameでフェードアウト後に遷移するシーンを指定してください。
		/// 
		/// Scene開始時のFadeInについてはisFadeinImageをTrueにすれば行います。
		/// ※ 他のスクリプトからこの画像をフェードインに使用するようにするためには、
		///    このスクリプトのStart()関数より早くこのフラグをTrueにしてください
		/// 
		/// Scene開始時以外でFadeInをスタートするのなら、isFadeinImageフラグをfalseにしてStartFadein()関数を任意のタイミングで実行してください。
		/// 
		/// </summary>
		///

		public enum FadeMode
		{
			NoneFade,
			FadeIn,
			FadeOut,
			FadeOn
		}

		// フェードの状態
		[HideInInspector]
		public FadeMode fadeModeStatus = FadeMode.NoneFade;

		// 関数の実行をどうするか
		private bool enableFunction = false;

		[Tooltip("Fadeにかける時間")]
		[SerializeField]
		private float FadeTime = 1f;

		// 内部用のフェード時間
		[Range(0,1)]
		private float alpha;

		private Image FadeImage;


		/* 他のスクリプトからこの画像をフェードインに使用するようにするためには、
		 このスクリプトのStart()関数より早くこのフラグをTrueにしてください。*/
		[Tooltip("シーン開始時にこの画像をフェードインに使用する")]
		public bool doFadeInSceneStart;

		private bool enableAlphaTop = false;

		private string sceneName;

		// 引数void、返値voidのdelegate
		public delegate void voidToVoidMesthod();



		// デリゲートを保存する変数
		public voidToVoidMesthod FadeOnMethod = null;
		public voidToVoidMesthod FadeOutMethod = null;
		// ---------------------------
		//FadeOutToAlpha
		public class FadeStopAlphaClass
		{
			public bool enableFunc = true;
			public float stopAlpha = 1f;
		}

		FadeStopAlphaClass FadeStopData;

		void Awake()
		{
		}

		void Start()
		{
			FadeImage = GetComponent<Image>();
            alpha = FadeImage.color.a;
            setAlpha(FadeImage, alpha);
            // この画像がフェードイン用画像ならフラグをセットする
            if (doFadeInSceneStart)
			{
				fadeModeStatus = FadeMode.FadeIn;
				FadeImage.enabled = true;
			}
		}

		void Update()
		{
			switch (fadeModeStatus)
			{
				case FadeMode.NoneFade:
					break;

				case FadeMode.FadeIn:
					FadeIn(FadeImage);
					break;

				case FadeMode.FadeOut:
					FadeOut(FadeImage);
					break;

				case FadeMode.FadeOn:
					FadeInAndOut(FadeImage);
					break;

				default:
					Debug.Log("FadeModeError");
					break;
			}
		}

		void setAlpha(Image image, float alpha)
		{
			image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
		}

		private void FadeOut(Image image)
		{
			alpha += Time.deltaTime / FadeTime;
			setAlpha(image, alpha);
			if (image.color.a >= 1f)
			{
				fadeModeStatus = FadeMode.NoneFade;
				if (enableFunction)
				{
					// フェードアウトした時の処理をここに書く
					if (FadeOutMethod != null) FadeOutMethod();
				}
			}
		}

		private void FadeIn(Image image)
		{
			alpha -= Time.deltaTime / FadeTime;
			
			setAlpha(image, alpha);	
			if (image.color.a <= 0f)	
			{
				alpha = 0f;
				FadeImage.enabled = false;
				fadeModeStatus = FadeMode.NoneFade;
			}
		}

		private void FadeInAndOut(Image image)
		{

			if (!enableAlphaTop)
			{
				alpha += Time.deltaTime / FadeTime;
			}
			else
			{
				alpha -= Time.deltaTime / FadeTime;
				if (image.color.a <= 0f)
				{
					fadeModeStatus = FadeMode.NoneFade;
					enableAlphaTop = false;
				}
			}
			setAlpha(image, alpha);
			if (image.color.a >= 1f)
			{
				alpha = 1.0f;
				enableAlphaTop = true;
				if (enableFunction)
				{
					enableFunction = false;
					// fadeOnの折り返しで実行する処理を記述
					if (FadeOnMethod != null) FadeOnMethod();
				}
			}
		}




		public void StartFadeIn()
		{
			if (fadeModeStatus == FadeMode.FadeIn) return;

            fadeModeStatus = FadeMode.FadeIn;
            setAlpha(FadeImage, 0f);
            FadeImage.enabled = true;	
		}

		void sceneChange()
		{
			if(sceneName != null) SceneManager.LoadScene(sceneName);
		}

		// シーン遷移を行うフェードアウト
		public void StartFadeOut(string changeSceneName)
		{
			enableFunction = true;
			this.sceneName = changeSceneName;
			FadeOutMethod = new voidToVoidMesthod(sceneChange);
			setAlpha(FadeImage, alpha);
			FadeImage.enabled = true;
			fadeModeStatus = FadeMode.FadeOut;
		}

		// シーン遷移を行わないフェードアウトスタート　ただしDelegateによる任意の関数を複数実行可能。
		// FadeOutMethod += function() で追加登録できます。　function は voidToVoidMesthod メソッド型ですので返値と引数もvoidでお願いします。
		public void StartFadeOut(bool enableDelegateMethod)
		{
			
			// fadeOut後にデリゲートに登録した関数群を実行するかどうか
			enableFunction = enableDelegateMethod;
			setAlpha(FadeImage, alpha);

			FadeImage.enabled = true;
			fadeModeStatus = FadeMode.FadeOut;
		}

		public void StartFadeOutToAlpha(FadeStopAlphaClass argFadeOutData)
		{

			// fadeOut後にデリゲートに登録した関数群を実行するかどうか
			enableFunction = argFadeOutData.enableFunc;

			FadeImage.enabled = true;
			fadeModeStatus = FadeMode.FadeOut;
		}

		/* FadeInAndOut
		 * 引数:　bool enableFunction=>フェードアウトとフェードインの間に関数を実行する場合trueを指定
		 * 
		 */
		public void StartFadeOn(bool enableFunction)
		{
			this.enableFunction = enableFunction;

			FadeImage.enabled = true;
			fadeModeStatus = FadeMode.FadeOn;
			enableAlphaTop = false;
		}
	}
}