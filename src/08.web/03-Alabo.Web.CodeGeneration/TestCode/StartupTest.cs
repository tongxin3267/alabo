using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Alabo.Framework.Core.Reflections.Services;
using File = System.IO.File;

namespace Alabo.Web.CodeGeneration.TestCode {

    /// <summary>
    ///     代码生成
    /// </summary>
    public class StartupTest {

        public void StartSingleServieCode(Type type, bool isBase = false, bool isZkcloud = false) {
            // type = typeof(IAutoReportService);

            // 参考Alabo.Linq.Dynamic.DynamicService.ResolveMethod
            var instanse = DynamicService.Resolve(type.Name);
            var instanseType = type;
            if (instanse == null) {
                instanseType = instanseType.GetType();
            }

            // var host = new TestHostingEnvironment();
            var testBuilder = new StringBuilder();
            var proj = string.Empty;
            if (type.Module.Name == "ZKCloud") {
                proj = type.Assembly.GetName().Name + ".Test";
            }

            proj = type.Assembly.GetName().Name.Replace(".App.", ".Test.");

            var paths = type.Namespace.Replace(type.Assembly.GetName().Name, "")
                .Split(".", StringSplitOptions.RemoveEmptyEntries);
            //  var filePath = $"{host.TestRootPath}\\{proj}";
            var filePath = proj;
            if (isZkcloud) {
                filePath += ".Test";
            }

            foreach (var item in paths) {
                filePath += "\\" + item;
                if (!Directory.Exists(filePath)) {
                    Directory.CreateDirectory(filePath);
                }
            }

            var fileName = $"{filePath}\\{type.Name}Tests.cs";
            if (isBase) {
                fileName = $"{filePath}\\{type.Name}BaseTests.cs";
            }

            var methods = instanseType.GetMethods(); // 当前服务所有的方法
            // 底层继承的方法不显示

            methods = methods.Where(r => r.Module.Name.Contains("ZKCloud")).ToArray();
            if (isZkcloud) {
                methods = methods.Where(r => r.Module.Name == "Alabo.dll").ToArray();
            } else {
                methods = methods.Where(r => r.Module.Name != "Alabo.dll").ToArray();
            }

            // methods = methods.Where(r => r.Name == "AddOrUpdate").ToArray();
            var namespaceList = new List<string>();
            if (File.Exists(fileName)) {
                using (var stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                    var text = string.Empty;
                    var reader = new StreamReader(stream);
                    text = reader.ReadToEnd();
                    //增量模式
                    var isAppend = false; // 是否增量增加

                    // 额外方法处理
                    if (isBase) {
                        var commonTest = TemplateServiceTest.CreateCommonTest(text, type.Name);
                        if (!commonTest.IsNullOrEmpty()) {
                            isAppend = true;
                            testBuilder.Append(commonTest);
                        }
                    } else {
                        foreach (var method in methods) {
                            if (HanderMethod(method) == null) {
                                continue;
                            }

                            var methodParameters = method.GetParameters(); // 方法参数
                            var name = method.Name + GetTestNameFromParameterInfo(methodParameters, out var param);

                            if (text.IndexOf($@"[TestMethod(""{name}"")]", StringComparison.OrdinalIgnoreCase) != -1) {
                                continue;
                            }

                            //生成测试方法
                            testBuilder.Append(CreateMethodTest(method, type));
                            isAppend = true;
                        }
                    }

                    testBuilder.AppendLine("/*end*/");
                    text = text.Replace("/*end*/", testBuilder.ToString());

                    if (isAppend) {
                        using (var writer = new StreamWriter(stream)) {
                            stream.Position = 0;
                            writer.Write(text);
                        }
                    }
                }

                return;
            }

            #region 命名空间处理

            testBuilder.AppendLine("using Xunit;");
            testBuilder.AppendLine($"using {type.Namespace};");
            testBuilder.AppendLine("using Alabo.Test.Core;");
            testBuilder.AppendLine("using System.Linq;");
            testBuilder.AppendLine(
                "using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;");
            testBuilder.AppendLine("using System.Collections.Generic;");
            testBuilder.AppendLine("using Alabo.Test.Base.Core.Model;");

            if (!isBase) {
                foreach (var method in methods) {
                    if (HanderMethod(method) == null) {
                        continue;
                    }

                    var methodParameters = method.GetParameters(); // 方法参数
                    var param = string.Empty;
                    foreach (var item in methodParameters) {
                        if (testBuilder.ToString()
                                .IndexOf(item.ParameterType.Namespace, StringComparison.OrdinalIgnoreCase) == -1) {
                            testBuilder.AppendLine($"using {item.ParameterType.Namespace};");
                        }
                    }
                }
            }

            testBuilder.AppendLine();
            testBuilder.AppendLine($"namespace {type.Namespace.Replace(".App.", ".Test.")} {{");
            testBuilder.AppendLine("using User = Alabo.App.Core.User.Domain.Entities.User;");
            testBuilder.AppendLine("using Product = Alabo.App.Shop.Product.Domain.Entities.Product;");
            testBuilder.AppendLine("using Order = Alabo.App.Shop.Order.Domain.Entities.Order;");
            testBuilder.AppendLine("using Store = Alabo.App.Shop.Store.Domain.Entities.Store;");
            testBuilder.AppendLine("");
            testBuilder.AppendLine("using UserType = App.Core.UserType.Domain.Entities.UserType;");

            #endregion 命名空间处理

            if (isBase) {
                testBuilder.AppendLine($"\tpublic class {type.Name}BaseTests : CoreTest {{");
            }

            testBuilder.AppendLine($"\tpublic class {type.Name}Tests : CoreTest {{");

            if (!isBase) {
                foreach (var method in methods) {
                    testBuilder.Append(CreateMethodTest(method, type));
                }
            }

            testBuilder.AppendLine("/*end*/");
            testBuilder.AppendLine("\t}");
            testBuilder.AppendLine("}");
            //创建文件

            using (var stream = File.Create(fileName)) {
                using (var writer = new StreamWriter(stream)) {
                    writer.Write(testBuilder);
                }
            }
        }

        /// <param name="method">The method.</param>
        /// <param name="type">The 类型.</param>
        private StringBuilder CreateMethodTest(MethodInfo method, Type type) {
            var testBuilder = new StringBuilder();
            if (HanderMethod(method) == null) {
                return testBuilder;
            }

            var methodParameters = method.GetParameters(); // 方法参数
            var name = method.Name + GetTestNameFromParameterInfo(methodParameters, out var param);

            //生成测试方法
            testBuilder.AppendLine("\t\t[Fact]");
            testBuilder.AppendLine($"\t\t[TestMethod(\"{name.Replace("`", "").Replace("[]", "")}\")]");
            testBuilder.AppendLine($"\t\tpublic void {name.Replace("`", "").Replace("[]", "")}_test () {{");
            foreach (var item in methodParameters) {
                // 泛型 ?类型
                var parameterType = item.ParameterType;
                if (item.ParameterType.Name.Contains("Nullable")) {
                    parameterType = item.ParameterType.GenericTypeArguments[0];
                }

                if (parameterType == typeof(int) ||
                    parameterType == typeof(long) ||
                    parameterType == typeof(decimal) ||
                    parameterType == typeof(float) ||
                    parameterType == typeof(double)) {
                    testBuilder.AppendLine($"\t\t\tvar {item.Name} = 0;");
                }

                if (parameterType == typeof(string)) {
                    testBuilder.AppendLine($"\t\t\tvar {item.Name} = \"\";");
                }

                if (parameterType == typeof(Guid)) {
                    testBuilder.AppendLine($"\t\t\tvar {item.Name} =Guid.Empty ;");
                }

                if (parameterType == typeof(bool)) {
                    testBuilder.AppendLine($"\t\t\tvar {item.Name} = false;");
                }

                if (parameterType.IsEnum) {
                    testBuilder.AppendLine($"\t\t\tvar {item.Name} = ({parameterType})0;");
                }

                if (parameterType is object) {
                    if (parameterType.IsGenericType) {
                        continue;
                    }

                    var parameterTypeName = parameterType.Name.Replace("&", "");
                    if (parameterTypeName.Replace("&", "").Equals("Int64", StringComparison.OrdinalIgnoreCase)) {
                        testBuilder.AppendLine($"\t\t\t{parameterTypeName} {item.Name} = 0;");
                    }

                    testBuilder.AppendLine($"\t\t\t{parameterTypeName} {item.Name} = null;");
                }
            }

            var methodName = method.Name;
            if (method.IsGenericMethod) {
                methodName = method.Name.Replace("`", "");
            }

            if (method.ReturnType != typeof(void)) {
                testBuilder.AppendLine(
                    $"\t\t\tvar result = Service<{type.Name}>().{methodName}({param.TrimStart(',')});");
                if (method.ReturnType == typeof(bool)) {
                    testBuilder.AppendLine("\t\t\tAssert.True(result);");
                }

                if (method.ReturnType == typeof(long) || method.ReturnType == typeof(int)) {
                    testBuilder.AppendLine("\t\t\tAssert.True(result>0);");
                }

                if (method.ReturnType == typeof(Guid)) {
                    testBuilder.AppendLine("\t\t\t Assert.True(result.IsGuidNullOrEmpty());");
                }

                testBuilder.AppendLine("\t\t\tAssert.NotNull(result);");
            }

            testBuilder.AppendLine($"\t\t\tService<{type.Name}>().{methodName}({param.TrimStart(',')});");

            testBuilder.AppendLine("\t\t}");
            testBuilder.AppendLine("");

            return testBuilder;
        }

        private MethodInfo HanderMethod(MethodInfo method) {
            if (method.IsSpecialName || method.IsConstructor || method.Name == "ToString" ||
                method.Name == "GetHashCode" ||
                method.Name == "Equals" || method.Name == "GetType" || method.IsPrivate ||
                method.Name == "Repository" || method.Name == "Service") {
                return null;
            }

            return method;
        }

        /// <summary>
        ///     根据参数获取名称以及参数
        ///     CheckUserExists_String_String_Int64
        /// </summary>
        /// <param name="methodParameters">The method parameters.</param>
        /// <param name="para">The para.</param>
        private string GetTestNameFromParameterInfo(ParameterInfo[] methodParameters, out string para) {
            var name = string.Empty;
            var param = string.Empty;
            foreach (var item in methodParameters) {
                var outString = item.GetCustomAttribute<OutAttribute>() == null ? string.Empty : "out";
                var parameterName = item.ParameterType.Name.Replace("&", "").Replace("`", "").Replace("[]", "");
                if (item.ParameterType.IsGenericType) {
                    if (item.ParameterType.BaseType == typeof(Expression) ||
                        item.ParameterType.BaseType == typeof(MulticastDelegate) ||
                        item.ParameterType.BaseType == typeof(LambdaExpression)
                    ) {
                        //if (item.ParameterType.Name.Contains("Dictionary")) {
                        //    param += $",new Dictionary<{string.Join(',', item.ParameterType.GenericTypeArguments.Select(o => o.UnderlyingSystemType.Namespace + "." + o.UnderlyingSystemType.Name))}>()";
                        //} else if (item.ParameterType.Name.Contains("IQuery")) {
                        //    param += $",new ExpressionQuery<{string.Join(',', item.ParameterType.GenericTypeArguments.Select(o => o.UnderlyingSystemType.Namespace + "." + o.UnderlyingSystemType.Name))}>()";
                        //} else {
                        //   // param += $",new List<{string.Join(',', item.ParameterType.GenericTypeArguments.Select(o => o.UnderlyingSystemType.Namespace + "." + o.UnderlyingSystemType.Name))}>()";
                        //}
                        param += $",{outString} null";
                        //param += $",{outString} o=> false";
                        name += $"_{parameterName}";
                    }
                    // 泛型 ?类型
                    else if (item.ParameterType.Name.Contains("Nullable")) {
                        name += $"_Nullable_{item.ParameterType.GenericTypeArguments[0]}";
                        //{ item.ParameterType.GenericTypeArguments[0]}?
                        param += $",{outString}  {item.Name}";
                    } else {
                        param += $",{outString} null";
                        //param +=
                        //    $",{outString} new List<{string.Join(',', item.ParameterType.GenericTypeArguments.Select(o => o.UnderlyingSystemType.Name))}>()";
                        name += $"_{parameterName}";
                    }
                } else {
                    name += $"_{parameterName}";
                    param += $",{outString} {item.Name}";
                }
            }

            para = param;
            return name.Replace(".", "_");
        }

        /// <summary>
        ///     Starts this instance.
        ///     开始生成单元测试代码
        /// </summary>

        public void Start() {
            Console.WriteLine(@"开始生成代码");
            var allServiceType = Ioc.Resolve<ITypeService>().GetAllServiceType(); //.Where(o=>!o.IsAbstract);
            foreach (var type in allServiceType) {
                try {
                    //  StartSingleServieCode(type);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        } /*end*/
    }
}