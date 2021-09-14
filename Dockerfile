# ========build 階段=========
# 使用微軟的 dotnet sdk:5.0 這個 image 作為 build 的環境
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

# 設定在 container 內的工作目錄為 /app
WORKDIR /app

# 將本機當前目錄下的所有東西(檔案及子目錄)複製到 container 的工作目錄(也就是/app)
COPY . ./

# 執行 dotnet restore 將所有用的 nuget package 安裝好
RUN dotnet restore

# 執行 dotnet publish 將 BookStore.Web 發佈到 dist 的目錄下
RUN dotnet publish -c Release -o dist

# ========執行階段=========
# 使用微軟的 aspnet:5.0 runtime 這個 image 作為執行環境
FROM mcr.microsoft.com/dotnet/aspnet:5.0

EXPOSE 80/tcp

# 設定在 container 內的工作目錄為 /app
WORKDIR /app

# 從 build-env 環境將 publish 好的 dkapi 複製到當前的工作目錄下(也就是/app)
COPY --from=build-env /app/dist .

# 設定當 container 被啟動時要執行的指令
ENTRYPOINT ["dotnet", "SlothBookStore.dll"]