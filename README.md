# Kaede Executor

Kaede Executor is a Roblox Executor for lua scripts made with [WeAreDevs](https://wearedevs.net/d/Exploit%20API) api. I am planning on adding more apis soon.

## Kaede Executor API

With Kaede Executor comes with a key sytems with locks the keys with the users HWID.

```json
{
    "key": "0000000000000000",
    "HWID": "00000000-0000-0000-0000-000000000000"
}
```

Also with our api the program also auto updates for user when you change the version and update the zip file for the new application and we will also have support for scripts to auto load with Executor.

```json
{
    "zip": "https://cdn.discordapp.com/attachments/000000000000000000/000000000000000000/Debug.zip",
    "version": "1.0.0.0",
    "Scripts": []
}
```

## Usage

You will need to go into the project **Kaede Executor.csproj** and change kaedeport.tk to localhost and run the API.
Then Run the Kaede Executor.exe.
