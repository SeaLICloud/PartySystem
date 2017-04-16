using System.Linq;
using System.Web.Mvc.Ajax;
using TomorrowSoft.Framework.Common.Domain;

namespace System.Web.Mvc.Html
{
    public static class UploadAndDownloadGeboExtensions
    {
        public static MvcHtmlString Download(this HtmlHelper htmlHelper, IContainFiles entity, string fileTitle = "")
        {
            var entityId = entity.GetType().GetProperty("Id").GetValue(entity, null).ToString();

            var containerDiv = new TagBuilder("div");
            var rowDiv = new TagBuilder("div");
            rowDiv.MergeAttribute("class", "row");
            foreach (var file in entity.GetFiles(fileTitle))
            {
                var colDiv = new TagBuilder("div");
                colDiv.MergeAttribute("class", "col-sm-12");

                var i = new TagBuilder("i");
                i.MergeAttribute("class", "glyphicon glyphicon-paperclip");
                var span = new TagBuilder("span");
                span.MergeAttribute("class", "text-muted");
                span.InnerHtml += string.Format("（{0}）", file.Size());
                colDiv.InnerHtml += i;
                colDiv.InnerHtml += "&nbsp;";
                var id = EntityFileIdentifier.of(entityId, file.Id);
                colDiv.InnerHtml += htmlHelper.ActionLink(file.Name, "DownloadFile", new { id });
                colDiv.InnerHtml += span;

                rowDiv.InnerHtml += colDiv;
            }
            containerDiv.InnerHtml += rowDiv;
            return new MvcHtmlString(containerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString Upload(this HtmlHelper htmlHelper, IContainFiles entity, string fileTitle = "")
        {
            var ajaxHelper = new AjaxHelper(htmlHelper.ViewContext, htmlHelper.ViewDataContainer);
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var entityId = entity.GetType().GetProperty("Id").GetValue(entity, null).ToString();
            var uploadButtonId = string.Format("upload{0}", Guid.NewGuid());

            var containerDiv = new TagBuilder("div");

            if (entity.GetFiles(fileTitle).Any())
            {
                var rowDiv = new TagBuilder("div");
                rowDiv.MergeAttribute("class", "row");
                foreach (var file in entity.GetFiles(fileTitle))
                {
                    var colDiv = new TagBuilder("div");
                    colDiv.MergeAttribute("class", "col-sm-12");

                    var i = new TagBuilder("i");
                    i.MergeAttribute("class", "glyphicon glyphicon-paperclip");
                    var span = new TagBuilder("span");
                    span.MergeAttribute("class", "text-muted");
                    span.InnerHtml += string.Format("（{0}）", file.Size());
                    colDiv.InnerHtml += i;
                    var id = EntityFileIdentifier.of(entityId, file.Id);
                    colDiv.InnerHtml += htmlHelper.ActionLink(file.Name, "DownloadFile", new {id});
                    colDiv.InnerHtml += span;
                    colDiv.InnerHtml += ajaxHelper.ActionLink("删除", "RemoveFile", new {id},
                                                              ajaxHelper.Options().Post().Confirm(string.Format("确认删除吗？")),
                                                              new {@class = "text-danger"});

                    rowDiv.InnerHtml += colDiv;
                }
                containerDiv.InnerHtml += rowDiv;
            }

            var iUploadButton = new TagBuilder("i");
            iUploadButton.MergeAttribute("class", "fa fa-upload");

            var uploadButton = new TagBuilder("a");
            uploadButton.MergeAttribute("class", "btn btn-default btn-xs");
            uploadButton.MergeAttribute("data-pjax", "");
            uploadButton.MergeAttribute("id", uploadButtonId);
            uploadButton.InnerHtml += iUploadButton;
            uploadButton.InnerHtml += " 点击上传";

            containerDiv.InnerHtml += uploadButton;
            containerDiv.InnerHtml += new TagBuilder("b");
            var scriptFormat = @"
<script type='text/javascript'>
    $(function () {{
        var uploader = new plupload.Uploader({{
            runtimes: 'html5,flash,silverlight,html4',
            browse_button: '{0}',
            url: '{1}',

            filters: {{
                max_file_size: '100mb',
                mime_types: [
                    {{ title: 'Image files', extensions: 'jpg' }},
                    {{ title: 'PDF files', extensions: 'pdf' }},
                    {{ title: 'Zip files', extensions: 'zip,rar' }}
                ]
            }},

            init: {{
                FilesAdded: function (up, files) {{
                    plupload.each(files, function (file) {{
                        uploader.start();
                        return false;
                    }});
                }},
                UploadProgress: function(up, file) {{
			        $('#{0}').next('b').html('<span>' + file.percent + '%</span>')
		        }},
                Error: function(up, err) {{
                    $.sticky(err.message, {{ autoclose: 5000, position: 'top-right', type: 'st-error' }});
		        }},
                FileUploaded: function (up, file, info) {{
                    $.sticky('上传成功', {{ autoclose: 5000, position: 'top-right', type: 'st-success' }});
                    $('#main_content').html(info.response);
                }},
            }},
        }});
        uploader.init();
    }})
</script>";
            containerDiv.InnerHtml += string.Format(scriptFormat, uploadButtonId, urlHelper.Action("UploadFile", new { id = entityId, title = fileTitle }));

            return new MvcHtmlString(containerDiv.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString UploadOrDownload(this HtmlHelper htmlHelper, bool showUpload, IContainFiles entity, string fileTitle = "")
        {
            return showUpload ? Upload(htmlHelper, entity, fileTitle) : Download(htmlHelper, entity, fileTitle);
        }
    }
}