using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Alabo.Extensions;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;

namespace Alabo.App.Core.Common.Controllers {

    [Route("/Web/[Action]")]
    public class UploadController : BaseController {

        /// <summary>
        ///     The host environment
        /// </summary>
        private readonly IHostingEnvironment _hostEnvironment;

        /// <summary>
        ///     The building list
        /// </summary>
        private readonly List<string> buildingList = new List<string>();

        /// <summary>
        ///     The result
        /// </summary>
        private readonly UploadResult Result = new UploadResult { State = UploadState.Unknown };

        /// <summary>
        ///     The file list
        /// </summary>
        private string[] FileList;

        /// <summary>
        ///     The size
        /// </summary>
        private int Size;

        /// <summary>
        ///     The start
        /// </summary>
        private int Start;

        /// <summary>
        ///     The state
        /// </summary>
        private ResultState State;

        /// <summary>
        ///     The total
        /// </summary>
        private int Total;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UploadController" /> class.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public UploadController(IHostingEnvironment hostingEnvironment) {
            _hostEnvironment = hostingEnvironment;
        }

        /// <summary>
        ///     Gets the path to list.
        /// </summary>
        /// <value>
        ///     The path to list.
        /// </value>
        public int PathToList { get; private set; }

        /// <summary>
        ///     Gets or sets the search extensions.
        /// </summary>
        /// <value>
        ///     The search extensions.
        /// </value>
        public string SearchExtensions { get; set; }

        /// <summary>
        ///     us the editor.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="action">The action.</param>
        public IActionResult UEditor([FromQuery] string callback, [FromQuery] string action) {
            if (!Response.Headers.ContainsKey("Access-Control-Allow-Origin")) {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }

            switch (action) {
                case "config":
                    if (!callback.IsNullOrEmpty()) {
                        var items = UEditorConfig.Items;

                        items["imageUrlPrefix"] = Request.IsHttps ? $"https://{Request.Host}/" : $"http://{Request.Host}/";
                        return new ContentResult() { Content = $"{callback}({items})" };
                    }
                    return WriteJson(callback, UEditorConfig.Items);

                case "uploadimage":
                    var config = new UploadConfig {
                        AllowExtensions = UEditorConfig.GetStringList("imageAllowFiles"),
                        PathFormat = UEditorConfig.GetString("imagePathFormat"),
                        SizeLimit = UEditorConfig.GetInt("imageMaxSize"),
                        UploadFieldName = UEditorConfig.GetString("imageFieldName")
                    };
                    return UploadFile(callback, config);

                case "uploadscrawl":
                    config = new UploadConfig {
                        AllowExtensions = new[] { ".png" },
                        PathFormat = UEditorConfig.GetString("scrawlPathFormat"),
                        SizeLimit = UEditorConfig.GetInt("scrawlMaxSize"),
                        UploadFieldName = UEditorConfig.GetString("scrawlFieldName"),
                        Base64 = true,
                        Base64Filename = "scrawl.png"
                    };
                    return UploadFile(callback, config);

                case "uploadvideo":
                    config = new UploadConfig {
                        AllowExtensions = UEditorConfig.GetStringList("videoAllowFiles"),
                        PathFormat = UEditorConfig.GetString("videoPathFormat"),
                        SizeLimit = UEditorConfig.GetInt("videoMaxSize"),
                        UploadFieldName = UEditorConfig.GetString("videoFieldName")
                    };
                    return UploadFile(callback, config);

                case "uploadfile":
                    config = new UploadConfig {
                        AllowExtensions = UEditorConfig.GetStringList("fileAllowFiles"),
                        PathFormat = UEditorConfig.GetString("filePathFormat"),
                        SizeLimit = UEditorConfig.GetInt("fileMaxSize"),
                        UploadFieldName = UEditorConfig.GetString("fileFieldName")
                    };
                    return UploadFile(callback, config);

                case "listimage":
                    try {
                        Start = string.IsNullOrEmpty(HttpContext.Request.Query["start"])
                            ? 0
                            : Convert.ToInt32(HttpContext.Request.Query["start"]);
                        Size = Convert.ToInt32(HttpContext.Request.Query["size"]);
                    } catch (FormatException) {
                        State = ResultState.InvalidParam;
                        WriteJson(callback, new { });
                    }

                    try {
                        var localPath = _hostEnvironment.ContentRootPath + "/upload/UEditor/image";
                        buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                            .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                            .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                        Total = buildingList.Count;
                        FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
                    } catch (UnauthorizedAccessException) {
                        State = ResultState.AuthorizError;
                    } catch (DirectoryNotFoundException) {
                        State = ResultState.PathNotFound;
                    } catch (IOException) {
                        State = ResultState.IOError;
                    }

                    return WriteJson(callback, new {
                        state = GetStateString(),
                        list = FileList?.Select(x => new { url = x }),
                        start = Start,
                        size = Size,
                        total = Total
                    });

                case "listfile":
                    try {
                        Start = string.IsNullOrEmpty(HttpContext.Request.Query["start"])
                            ? 0
                            : Convert.ToInt32(HttpContext.Request.Query["start"]);
                        Size = Convert.ToInt32(HttpContext.Request.Query["size"]);
                    } catch (FormatException) {
                        State = ResultState.InvalidParam;
                        WriteJson(callback, new { });
                    }

                    try {
                        var localPath = _hostEnvironment.ContentRootPath + "/upload/UEditor/file";
                        buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                            .Where(x => SearchExtensions.Contains(Path.GetExtension(x).ToLower()))
                            .Select(x => PathToList + x.Substring(localPath.Length).Replace("\\", "/")));
                        Total = buildingList.Count;
                        FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
                    } catch (UnauthorizedAccessException) {
                        State = ResultState.AuthorizError;
                    } catch (DirectoryNotFoundException) {
                        State = ResultState.PathNotFound;
                    } catch (IOException) {
                        State = ResultState.IOError;
                    }

                    return WriteJson(callback, new {
                        state = GetStateString(),
                        list = FileList?.Select(x => new { url = x }),
                        start = Start,
                        size = Size,
                        total = Total
                    });

                case "catchimage":
                    StringValues sources;
                    Request.Form.TryGetValue("source[]", out sources);
                    if (sources.Count == 0) {
                        return WriteJson(callback, new { state = "参数错误：没有指定抓取源" });
                    }

                    var Crawlers = sources.Select(x => new Crawler(x).Fetch(_hostEnvironment)).ToArray();
                    return WriteJson(callback, new {
                        state = "SUCCESS",
                        list = Crawlers.Select(x => new {
                            state = x.State,
                            source = x.SourceUrl,
                            url = x.ServerUrl
                        })
                    });

                default:
                    return WriteJson(callback, new { state = "action 参数为空或者 action 不被支持。" });
            }
        }

        /// <summary>
        ///     Uploads the file.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="config">The configuration.</param>
        private IActionResult UploadFile(string callback, UploadConfig config) {
            config.PathFormat = "wwwroot/" + config.PathFormat.Replace("upload", "uploads");
            var localPath = string.Empty;

            byte[] uploadFileBytes = null;
            string uploadFileName = null;
            if (config.Base64) {
                uploadFileName = config.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request.Query[config.UploadFieldName]);
            } else {
                var file = Request.Form.Files[config.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName, config)) {
                    Result.State = UploadState.TypeNotAllow;
                    return WriteJson(callback, new {
                        state = GetStateMessage(Result.State),
                        url = Result.Url,
                        title = Result.OriginFileName,
                        original = Result.OriginFileName,
                        error = Result.ErrorMessage
                    });
                }

                if (!CheckFileSize((int)file.Length, config)) {
                    Result.State = UploadState.SizeLimitExceed;
                    return WriteJson(callback, new {
                        state = GetStateMessage(Result.State),
                        url = Result.Url,
                        title = Result.OriginFileName,
                        original = Result.OriginFileName,
                        error = Result.ErrorMessage
                    });
                }

                uploadFileBytes = new byte[file.Length];
                try {
                    file.OpenReadStream().Read(uploadFileBytes, 0, (int)file.Length);
                } catch (Exception) {
                    Result.State = UploadState.NetworkError;
                    return WriteJson(callback, new {
                        state = GetStateMessage(Result.State),
                        url = Result.Url,
                        title = Result.OriginFileName,
                        original = Result.OriginFileName,
                        error = Result.ErrorMessage
                    });
                }
            }

            Result.OriginFileName = uploadFileName;
            var savePath = PathFormatter.Format(uploadFileName, config.PathFormat);
            localPath = _hostEnvironment.ContentRootPath + "/" + savePath;
            try {
                if (!Directory.Exists(Path.GetDirectoryName(localPath))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }

                System.IO.File.WriteAllBytes(localPath, uploadFileBytes);
                var extension = Path.GetExtension(uploadFileName).ToLower();
                if (extension == ".jpg" ||
                    extension == ".png" ||
                    extension == ".jpeg" ||
                    extension == ".gif" ||
                    extension == ".bmp") {
                    var iSource = Image.FromFile(localPath);
                    if (iSource.PhysicalDimension.Width > 1024) {
                        int dHeight = 64, dWidth = 64;
                        var rate = iSource.Width * 1.00 / iSource.Height;
                        if (rate <= 1) {
                            dHeight = 1024;
                            dWidth = (int)(rate * 1024);
                        } else {
                            dWidth = 1024;
                            dHeight = (int)(1024 / rate);
                        }

                        var tFormat = iSource.RawFormat;
                        int sW = dWidth, sH = dHeight;
                        var ob = new Bitmap(dWidth, dHeight);
                        var g = Graphics.FromImage(ob);
                        g.Clear(Color.WhiteSmoke);
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0,
                            iSource.Width, iSource.Height, GraphicsUnit.Pixel);
                        g.Dispose();
                        iSource.Dispose();
                        //以下代码为保存图片时，设置压缩质量
                        var ep = new EncoderParameters();
                        var qy = new long[1];
                        qy[0] = 90; //设置压缩的比例1-100
                        var eParam = new EncoderParameter(Encoder.Quality, qy);
                        ep.Param[0] = eParam;
                        try {
                            var arrayICI = ImageCodecInfo.GetImageEncoders();
                            ImageCodecInfo jpegICIinfo = null;
                            for (var x = 0; x < arrayICI.Length; x++) {
                                if (arrayICI[x].FormatDescription.Equals("JPEG")) {
                                    jpegICIinfo = arrayICI[x];
                                    break;
                                }
                            }

                            if (System.IO.File.Exists(localPath)) {
                                System.IO.File.Delete(localPath);
                            }

                            if (jpegICIinfo != null) {
                                ob.Save(localPath, jpegICIinfo, ep); //dFile是压缩后的新路径
                            } else {
                                ob.Save(localPath, tFormat);
                            }
                        } catch (Exception ex) {
                            Console.Write(ex.Message);
                        } finally {
                            iSource.Dispose();
                            ob.Dispose();
                        }
                    }
                }

                Result.Url = savePath;
                Result.State = UploadState.Success;
            } catch (Exception e) {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }

            return WriteJson(callback, new {
                state = GetStateMessage(Result.State),
                url = Result.Url,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage
            });
        }

        /// <summary>
        ///     Gets the state message.
        /// </summary>
        /// <param name="state">The state.</param>
        private string GetStateMessage(UploadState state) {
            switch (state) {
                case UploadState.Success:
                    return "SUCCESS";

                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";

                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";

                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";

                case UploadState.NetworkError:
                    return "网络错误";
            }

            return "未知错误";
        }

        /// <summary>
        ///     Checks the type of the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="config">The configuration.</param>
        private bool CheckFileType(string filename, UploadConfig config) {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return config.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        /// <summary>
        ///     Checks the size of the file.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="config">The configuration.</param>
        private bool CheckFileSize(int size, UploadConfig config) {
            return size < config.SizeLimit;
        }

        /// <summary>
        ///     Writes the json.
        /// </summary>
        /// <param name="callback">The callback.</param>
        /// <param name="data">The data.</param>
        private JsonResult WriteJson(string callback, object data) {
            JsonObject o = data as JsonObject;
            return Json(data);
        }

        /// <summary>
        ///     Builds the json.
        /// </summary>
        /// <param name="info">The information.</param>
        [NonAction]
        private string BuildJson(Hashtable info) {
            var fields = new List<string>();
            var keys = new[] { "originalName", "name", "url", "size", "state", "type" };
            for (var i = 0; i < keys.Length; i++) {
                fields.Add(string.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
            }

            return "{" + string.Join(",", fields) + "}";
        }

        /// <summary>
        ///     Gets the state string.
        /// </summary>
        private string GetStateString() {
            switch (State) {
                case ResultState.Success:
                    return "SUCCESS";

                case ResultState.InvalidParam:
                    return "参数不正确";

                case ResultState.PathNotFound:
                    return "路径不存在";

                case ResultState.AuthorizError:
                    return "文件系统权限不足";

                case ResultState.IOError:
                    return "文件系统读取错误";
            }

            return "未知错误";
        }

        /// <summary>
        ///     Uploads the image URL.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="responseType">Type of the response.</param>
        public IActionResult UploadImageUrl([FromServices] IHostingEnvironment env, string responseType) {
            // CKEditor提交的很重要的一个参数
            string callback = Request.Query["CKEditorFuncNum"];
            var form = Request.Form;
            var img = form.Files[0]; //获取图片
            var fileName = img.FileName;
            var extension = Path.GetExtension(img.FileName);
            var openReadStream = img.OpenReadStream();
            var buff = new byte[openReadStream.Length];
            openReadStream.ReadAsync(buff, 0, buff.Length);
            var filenameGuid = Guid.NewGuid().ToString();
            var fileoutName = "Upload/" + filenameGuid + extension;
            //var bowerPath = Path.Combine(WebContext.Config.AppBaseDirectory, fileoutName);//获取到图片保存的路径，这边根据自己的实现
            var savePath = Path.Combine(env.WebRootPath, fileoutName);
            using (var fs = new FileStream(savePath, FileMode.OpenOrCreate)) {
                fs.WriteAsync(buff, 0, buff.Length);
                fs.Close();
            }

            if (responseType == "json") {
                return Json(new {
                    uploaded = 1,
                    fileName = img.FileName,
                    url = "/wwwroot/" + fileoutName
                });
            }

            var result =
                $"<script type=\"text/javascript\">window.parent.CKEDITOR.tools.callFunction(\"{callback}\", \"{"/" + fileoutName}\", \"\");</script>";
            Response.ContentType = "text/html;charset=UTF-8";
            return Content(result);
        }

        /// <summary>
        ///     Uploads the image URL paste.
        /// </summary>
        /// <param name="env">The env.</param>
        [HttpPost]
        public IActionResult UploadImageUrlPaste([FromServices] IHostingEnvironment env) {
            // CKEditor提交的很重要的一个参数
            string callback = Request.Query["CKEditorFuncNum"];
            var form = Request.Form;
            var img = form.Files[0]; //获取图片
            var fileName = img.FileName;
            var extension = Path.GetExtension(img.FileName);
            var openReadStream = img.OpenReadStream();
            var buff = new byte[openReadStream.Length];
            openReadStream.ReadAsync(buff, 0, buff.Length);
            var filenameGuid = Guid.NewGuid().ToString();
            var fileoutName = "upload/" + filenameGuid + extension;
            var savePath = Path.Combine(env.WebRootPath, fileoutName);
            using (var fs = new FileStream(savePath, FileMode.Create)) {
                fs.WriteAsync(buff, 0, buff.Length);
            }

            return Json(new {
                uploaded = 1,
                fileName = img.FileName,
                url = fileoutName
            });
        }

        /// <summary>
        ///     Uploads this instance.
        /// </summary>
        public ActionResult Upload() {
            //上传配置
            var pathbase = "/wwwroot/uploads/editor/"; //保存路径
            var size = 10; //文件大小限制,单位mb
            var fileType = ".jpg,.jpeg,.png,.gif,.bmp,.rar,.zip,.7z,.ico,.icon";

            string callback = Request.Form["callback"];
            string editorId = Request.Form["editorid"];

            var info = Resolve<IStorageFileService>().Upload(Request.Form.Files, pathbase);

            var json = info.ToJsons();
            Response.ContentType = "text/html";
            if (callback != null) {
                return Content($"<script>{callback}(Json.parse(\"{json}\"))</script>");
            }

            return Content(json);
        }

        /// <summary>
        /// </summary>
        private enum ResultState {

            /// <summary>
            ///     The success
            /// </summary>
            Success,

            /// <summary>
            ///     The invalid parameter
            /// </summary>
            InvalidParam,

            /// <summary>
            ///     The authoriz error
            /// </summary>
            AuthorizError,

            /// <summary>
            ///     The io error
            /// </summary>
            IOError,

            /// <summary>
            ///     The path not found
            /// </summary>
            PathNotFound
        }
    }

    /// <summary>
    /// </summary>
    public class Crawler {

        /// <summary>
        ///     Initializes a new instance of the <see cref="Crawler" /> class.
        /// </summary>
        /// <param name="sourceUrl">The source URL.</param>
        public Crawler(string sourceUrl) {
            SourceUrl = sourceUrl;
        }

        /// <summary>
        ///     Gets or sets the source URL.
        /// </summary>
        /// <value>
        ///     The source URL.
        /// </value>
        public string SourceUrl { get; set; }

        /// <summary>
        ///     Gets or sets the server URL.
        /// </summary>
        /// <value>
        ///     The server URL.
        /// </value>
        public string ServerUrl { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        ///     Fetches the specified hosting environment.
        /// </summary>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        public Crawler Fetch(IHostingEnvironment hostingEnvironment) {
            if (!IsExternalIPAddress(SourceUrl)) {
                State = "INVALID_URL";
                return this;
            }

            var request = WebRequest.Create(SourceUrl) as HttpWebRequest;
            using (var response = request.GetResponseAsync().Result as HttpWebResponse) {
                if (response.StatusCode != HttpStatusCode.OK) {
                    State = "Url returns " + response.StatusCode + ", " + response.StatusDescription;
                    return this;
                }

                if (response.ContentType.IndexOf("image") == -1) {
                    State = "Url is not an image";
                    return this;
                }

                ServerUrl = PathFormatter.Format(Path.GetFileName(SourceUrl),
                    UEditorConfig.GetString("catcherPathFormat"));
                var savePath = hostingEnvironment.WebRootPath + "/" + ServerUrl;
                if (!Directory.Exists(Path.GetDirectoryName(savePath))) {
                    Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                }

                try {
                    var stream = response.GetResponseStream();
                    var reader = new BinaryReader(stream);
                    byte[] bytes;
                    using (var ms = new MemoryStream()) {
                        var buffer = new byte[4096];
                        int count;
                        while ((count = reader.Read(buffer, 0, buffer.Length)) != 0) {
                            ms.Write(buffer, 0, count);
                        }

                        bytes = ms.ToArray();
                    }

                    File.WriteAllBytes(savePath, bytes);
                    State = "SUCCESS";
                } catch (Exception e) {
                    State = "抓取错误：" + e.Message;
                }

                return this;
            }
        }

        /// <summary>
        ///     Determines whether [is external ip address] [the specified URL].
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>
        ///     <c>true</c> if [is external ip address] [the specified URL]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsExternalIPAddress(string url) {
            var uri = new Uri(url);
            switch (uri.HostNameType) {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntryAsync(uri.DnsSafeHost).Result;
                    foreach (var ipAddress in ipHostEntry.AddressList) {
                        var ipBytes = ipAddress.GetAddressBytes();
                        if (ipAddress.AddressFamily == AddressFamily.InterNetwork) {
                            if (!IsPrivateIP(ipAddress)) {
                                return true;
                            }
                        }
                    }

                    break;

                case UriHostNameType.IPv4:
                    return !IsPrivateIP(IPAddress.Parse(uri.DnsSafeHost));
            }

            return false;
        }

        /// <summary>
        ///     Determines whether [is private ip] [the specified my ip address].
        /// </summary>
        /// <param name="myIPAddress">My ip address.</param>
        /// <returns>
        ///     <c>true</c> if [is private ip] [the specified my ip address]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPrivateIP(IPAddress myIPAddress) {
            if (IPAddress.IsLoopback(myIPAddress)) {
                return true;
            }

            if (myIPAddress.AddressFamily == AddressFamily.InterNetwork) {
                var ipBytes = myIPAddress.GetAddressBytes();
                // 10.0.0.0/24
                if (ipBytes[0] == 10) {
                    return true;
                }
                // 172.16.0.0/16

                if (ipBytes[0] == 172 && ipBytes[1] == 16) {
                    return true;
                }
                // 192.168.0.0/16

                if (ipBytes[0] == 192 && ipBytes[1] == 168) {
                    return true;
                }
                // 169.254.0.0/16

                if (ipBytes[0] == 169 && ipBytes[1] == 254) {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// </summary>
    public class UploadConfig {

        /// <summary>
        ///     文件命名规则
        /// </summary>
        /// <value>
        ///     The path format.
        /// </value>
        public string PathFormat { get; set; }

        /// <summary>
        ///     上传表单域名称
        /// </summary>
        /// <value>
        ///     The name of the upload field.
        /// </value>
        public string UploadFieldName { get; set; }

        /// <summary>
        ///     上传大小限制
        /// </summary>
        /// <value>
        ///     The size limit.
        /// </value>
        public int SizeLimit { get; set; }

        /// <summary>
        ///     上传允许的文件格式
        /// </summary>
        /// <value>
        ///     The allow extensions.
        /// </value>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        ///     文件是否以 Base64 的形式上传
        /// </summary>
        /// <value>
        ///     <c>true</c> if base64; otherwise, <c>false</c>.
        /// </value>
        public bool Base64 { get; set; }

        /// <summary>
        ///     Base64 字符串所表示的文件名
        /// </summary>
        /// <value>
        ///     The base64 filename.
        /// </value>
        public string Base64Filename { get; set; }
    }

    /// <summary>
    /// </summary>
    public class UploadResult {

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        public UploadState State { get; set; }

        /// <summary>
        ///     Gets or sets the URL.
        /// </summary>
        /// <value>
        ///     The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the name of the origin file.
        /// </summary>
        /// <value>
        ///     The name of the origin file.
        /// </value>
        public string OriginFileName { get; set; }

        /// <summary>
        ///     Gets or sets the error message.
        /// </summary>
        /// <value>
        ///     The error message.
        /// </value>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// </summary>
    [ClassProperty(Name = "上传状态")]
    public enum UploadState {

        /// <summary>
        ///     The success
        /// </summary>
        Success = 0,

        /// <summary>
        ///     The size limit exceed
        /// </summary>
        SizeLimitExceed = -1,

        /// <summary>
        ///     The type not allow
        /// </summary>
        TypeNotAllow = -2,

        /// <summary>
        ///     The file access error
        /// </summary>
        FileAccessError = -3,

        /// <summary>
        ///     The network error
        /// </summary>
        NetworkError = -4,

        /// <summary>
        ///     The unknown
        /// </summary>
        Unknown = 1
    }

    /// <summary>
    /// </summary>
    public static class PathFormatter {

        /// <summary>
        ///     Formats the specified origin file name.
        /// </summary>
        /// <param name="originFileName">Name of the origin file.</param>
        /// <param name="pathFormat">The path format.</param>
        public static string Format(string originFileName, string pathFormat) {
            if (string.IsNullOrWhiteSpace(pathFormat)) {
                pathFormat = "{filename}{rand:6}";
            }

            var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
            originFileName = invalidPattern.Replace(originFileName, "");

            var extension = Path.GetExtension(originFileName);
            var filename = Path.GetFileNameWithoutExtension(originFileName);

            pathFormat = pathFormat.Replace("{filename}", filename);
            pathFormat = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(pathFormat,
                delegate (Match match) {
                    var digit = 6;
                    if (match.Groups.Count > 2) {
                        digit = Convert.ToInt32(match.Groups[2].Value);
                    }

                    var rand = new Random();
                    return rand.Next((int)Math.Pow(10, digit), (int)Math.Pow(10, digit + 1)).ToString();
                });

            pathFormat = pathFormat.Replace("{time}", DateTime.Now.Ticks.ToString());
            pathFormat = pathFormat.Replace("{yyyy}", DateTime.Now.Year.ToString());
            pathFormat = pathFormat.Replace("{yy}", (DateTime.Now.Year % 100).ToString("D2"));
            pathFormat = pathFormat.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
            pathFormat = pathFormat.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
            pathFormat = pathFormat.Replace("{hh}", DateTime.Now.Hour.ToString("D2"));
            pathFormat = pathFormat.Replace("{ii}", DateTime.Now.Minute.ToString("D2"));
            pathFormat = pathFormat.Replace("{ss}", DateTime.Now.Second.ToString("D2"));

            return pathFormat + extension;
        }
    }
}