# Kogane Slack

Slack にメッセージを送信するクラス

## 使用例 1

```cs
var url = "【Webhook URL】";

var payload = new Payload
{
	text = "【ここにテキスト】",
};

var routine = IncomingWebhooks.SendMessage( url, payload );
StartCoroutine( routine );
```

## 使用例 2

```cs
var url = "【Webhook URL】";

var payload = new Payload
{
	text = "<http://baba-s.hatenablog.com/|ブログ>を表示します",
};

var routine = IncomingWebhooks.SendMessage( url, payload );
StartCoroutine( routine );
```

## 使用例 3

```cs
var url = "【Webhook URL】";

var payload = new Payload
{
	text		= "【ここにテキスト】",
	username	= "【ここにユーザー名】", 
	icon_url	= "【ここにアイコン画像の URL】", 
};

var routine = IncomingWebhooks.SendMessage( url, payload );
StartCoroutine( routine );
```

## 使用例 4

```cs
var url = "【Webhook URL】";

var attachment = new Attachment
{
	fallback	= "エラーが発生しました", 
	color		= "#D00000", 
	pretext		= "エラーが発生しました",
	text		= error, 
};

var payload = new Payload
{
	username	= "【ここにユーザー名】", 
	icon_url	= "【ここにアイコン画像の URL】", 
	attachments	= new [] { attachment }, 
};

var routine = IncomingWebhooks.SendMessage( url, payload );
StartCoroutine( routine );
```
