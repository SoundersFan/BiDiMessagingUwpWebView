# Bidirectional Communications between UWP <==> WebView

## What is this?
This is a sample that shows how a UWP app can host a WebView with content that can communicate with the hosting UWP app and vise-versa. 

## Build
* Install Visual Studio 2019 with UWP workload
* `git clone https://github.com/SoundersFan/BiDiMessagingUwpWebView`
* `cd BiDiMessagingUwpWebView`
* `start BiDiMessagingUwpWebView.sln`
* Press F5

## WebView to UWP:
To communicate from the WebView content to the UWP app, the UWP app needs to be setup to do this an a perscribed way with a named object, for example *host*. Set up the app:
1. In the XAML, handle the [WebView.NavigationStarting](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.navigationstarting) ([link to code](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/MainPage.xaml#L22)) event and in it call the [WebView.AddWebAllowedObject](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.addweballowedobject) ([link to code](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/MainPage.xaml.cs#L25)) passing in a name (I use "++host++") and the Universal Windows Component to call into.
2. Add a Universal Windows Component to call into (I called it [WebHost](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebViewComponent/WebHost.cs#L9)). **Important**: This component class needs to be in a separate Universal Windows Component library, separate from the App's project. It also must be tagged with the [AllowForWeb](https://docs.microsoft.com/en-us/uwp/api/windows.foundation.metadata.allowforwebattribute) attribute ([link to code](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebViewComponent/WebHost.cs#L8)).
3. Add the [ApplicationContentUriRules](https://docs.microsoft.com/en-us/uwp/schemas/appxpackage/uapmanifestschema/element-uap-applicationcontenturirules) to the Package.appxmanifest ([link to code](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/Package.appxmanifest#L43-L45)).
4. In the JavaScript, call [window.++host++.incrementHostCounter()](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/Assets/Page.html#L20). Parameters can be added to the call to send data. In this sample, the target of the call fires an event that the MainPage is listeing to which, in turn, updates the counter on the UWP side.

## UWP to WebView
To communicate from the UWP app to the WebView content, it's best for the content's JavaScript to have functions that receive the message.
1. Add methods to the page's JavaScript, such as [incrementWebCounter()](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/Assets/Page.html#L10), to receive the message.
2. Call the [WebView.InvokeScriptAsync](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.invokescriptasync)("eval", new [] { "incrementWebCounter()" }) ([link to code](https://github.com/SoundersFan/BiDiMessagingUwpWebView/blob/master/BiDiMessagingUwpWebView/MainPage.xaml.cs#L29)) to send the message. Parameters can be added to the call to send data.

## Extras
In the final version of the code there is another set of bidirectional communications that sends data (a string to be specific) in both directions. The button click count will stay in sync since clicking on either button will update the other. Here the "From WebView" was typed into the html input text and the button was press to send that to the UWP. Conversly, the "From UWP" was typed into the TextBlock and the button was press to send that to the WebView.

![Screen capture of the application. WebView on top hald and UWP XAML content on the bottom. Both contents have a button that when pressed increment their own counters and send messages to the other to increment the others. There are also buttons and text inputs that do a similar thing but with message strings.](ScreenShot.png)
