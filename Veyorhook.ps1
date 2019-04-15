$STATUS = $args[0]
$WEBHOOK_URL = $args[1]

if (!$WEBHOOK_URL) {
  Write-Output "Webhook URL is missing. Please make sure a valid Webhook URL is provided."
  Exit
}

$AVATAR = "https://i.imgur.com/cCh2naD.png"
$USERNAME = "RaiderIOSharp"

Switch ($STATUS) {
  "success" {
    $EMBED_COLOR = 7536470
    $DESC = "Appveyor successfully built"    
    $THUMB = "https://i.imgur.com/86yoUgS.png"
    Break
  }
  "failure" {    
    $EMBED_COLOR = 16007775
    $THUMB = "https://i.imgur.com/HHFIGBx.png"
    $DESC = "Appveyor encountered an error when building"
    Break
  }
  default {
    $THUMB = $AVATAR
    $EMBED_COLOR = 16777215    
    $DESC = "Appveyor came across a massive hmm when dealing with"    
    Break
  }
}

$WEBHOOK_DATA="{
  ""username"": ""$USERNAME"",
  ""avatar_url"": ""$AVATAR"",
  ""embeds"": [ {
    ""color"": $EMBED_COLOR,
    ""thumbnail"": {
      ""url"": ""$THUMB""
    },
    ""description"": ""$DESC `[$env:APPVEYOR_REPO_NAME](https://ci.appveyor.com/project/$env:APPVEYOR_ACCOUNT_NAME/$env:APPVEYOR_PROJECT_NAME)` repository on ` $env:APPVEYOR_REPO_BRANCH ` branch. $env:APPVEYOR_REPO_NAME build version is $env:APPVEYOR_BUILD_VERSION with $env:APPVEYOR_BUILD_ID build ID."",
    ""fields"": [
      {
        ""name"": ""Commit Information"",
        ""value"": ""**Author:** $env:APPVEYOR_REPO_COMMIT_AUTHOR\n**SHA:** [``$($env:APPVEYOR_REPO_COMMIT.substring(0, 20))``](https://github.com/$env:APPVEYOR_REPO_NAME/commit/$env:APPVEYOR_REPO_COMMIT)\n**Message:** $env:APPVEYOR_REPO_COMMIT_MESSAGE""
      }
    ]
  } ]
}"

Invoke-RestMethod -Uri "$WEBHOOK_URL" -Method "POST" -UserAgent "AppVeyorHook" `
-ContentType "application/json" -Header @{"X-Author"="Draxis"} -Body $WEBHOOK_DATA