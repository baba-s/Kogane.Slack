using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Kogane.Slack
{
    [Serializable]
    public sealed class PostMessageData
    {
        public string token      = string.Empty;
        public string channel    = string.Empty;
        public string text       = string.Empty;
        public string parse      = string.Empty;
        public string link_names = string.Empty;
        public string username   = string.Empty;
        public string icon_url   = string.Empty;
        public string icon_emoji = string.Empty;
    }

    [Serializable]
    public sealed class UploadData
    {
        public string token           = string.Empty;
        public string filename        = string.Empty;
        public string title           = string.Empty;
        public string initial_comment = string.Empty;
        public string channels        = string.Empty;
    }

    /// <summary>
    /// Slack API を使用して Slack にメッセージやスクリーンショットを送信するクラス
    /// </summary>
    public static class SlackAPI
    {
        //================================================================================
        // 関数（static）
        //================================================================================
        /// <summary>
        /// Slack にメッセージを送信します
        /// </summary>
        public static IEnumerator PostMessage
        (
            PostMessageData data,
            Action          onSuccess = null,
            Action<string>  onError   = null
        )
        {
            var form = new WWWForm();
            form.AddField( "token", data.token );
            form.AddField( "channel", data.channel );
            form.AddField( "text", data.text );
            form.AddField( "parse", data.parse );
            form.AddField( "link_names", data.link_names );
            form.AddField( "username", data.username );
            form.AddField( "icon_url", data.icon_url );
            form.AddField( "icon_emoji", data.icon_emoji );

            var url     = "https://slack.com/api/chat.postMessage";
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

        /// <summary>
        /// Slack にスクリーンショットを送信します
        /// </summary>
        public static IEnumerator UploadScreenShot
        (
            UploadData     data,
            Action         onSuccess = null,
            Action<string> onError   = null
        )
        {
            yield return new WaitForEndOfFrame();

            var width   = Screen.width;
            var height  = Screen.height;
            var texture = new Texture2D( width, height, TextureFormat.ARGB32, false );
            var source  = new Rect( 0, 0, width, height );

            texture.ReadPixels( source, 0, 0 );
            texture.Apply();

            var form     = new WWWForm();
            var contents = texture.EncodeToPNG();

            form.AddField( "token", data.token );
            form.AddField( "title", data.title );
            form.AddField( "initial_comment", data.initial_comment );
            form.AddField( "channels", data.channels );

            form.AddBinaryData( "file", contents, data.filename, "image/png" );

            var url     = "https://slack.com/api/files.upload";
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