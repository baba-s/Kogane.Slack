using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace UniSlack
{
	[Serializable]
	public sealed class Payload
	{
		public string       channel;
		public string       username;
		public string       text;
		public string       icon_emoji;
		public string       icon_url;
		public Attachment[] attachments;
	}

	[Serializable]
	public sealed class Attachment
	{
		public string   fallback;
		public string   color;
		public string   pretext;
		public string   author_name;
		public string   author_link;
		public string   author_icon;
		public string   title;
		public string   title_link;
		public string   text;
		public Field[]  fields;
		public string   image_url;
		public string   thumb_url;
		public string   footer;
		public string   footer_icon;
		public string   ts;
		public string[] mrkdwn_in;
	}

	[Serializable]
	public sealed class Field
	{
		public string title;
		public string value;
		public string @short;
	}

	/// <summary>
	/// Incoming Webhooks を使用して Slack にメッセージを送信するクラス
	/// </summary>
	public static class IncomingWebhooks
	{
		//================================================================================
		// 関数（static）
		//================================================================================
		/// <summary>
		/// Slack にメッセージを送信します
		/// </summary>
		public static IEnumerator SendMessage
		(
			string         url,
			Payload        payload,
			Action         onSuccess = null,
			Action<string> onError   = null
		)
		{
			var form = new WWWForm();
			form.AddField( "payload", JsonUtility.ToJson( payload ) );

			var request = UnityWebRequest.Post( url, form );
			yield return request.SendWebRequest();
			var error = request.error;

			if ( !string.IsNullOrEmpty( error ) )
			{
				onError?.Invoke( error );
				yield break;
			}

			onSuccess?.Invoke();
		}
	}
}