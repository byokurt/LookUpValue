using OsmanKURT.Business.Contracts;
using OsmanKURT.Cache;
using OsmanKURT.Data.Contracts;
using OsmanKURT.Log;
using System;
using Microsoft.Extensions.DependencyInjection;
using OsmanKURT.Business.Entities;
using OsmanKURT.ClientEntites;
using OsmanKURT.Common;
using OsmanKURT.Mail;
using System.Net.Mail;
using System.Collections.Generic;
using System.Globalization;

namespace OsmanKURT.Business
{
    public class LookuUpValueEngine : ILookUpValueEngine
    {
        IServiceProvider _collection;
        ICacheManager _cache;
        ILogManager _logger;
        IMailManager _mail;

        public LookuUpValueEngine(IMailManager mail, IServiceProvider collection, ICacheManager cache, ILogManager logger)
        {
            _mail = mail;
            _collection = collection;
            _cache = cache;
            _logger = logger;
        }

        public LookuUpValueEngine(IServiceProvider collection, ICacheManager cache)
        {
            _collection = collection;
            _cache = cache;
        }

        /// <summary>
        /// Parametreye ait data öncelik Redis olmak şartıyla PostgreSQL üzerinden çekilmesi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetValue(GetValueRequest request)
        {
            new GetValueControl().Validate(request);

            var response = _cache.ExecuteCached(string.Format("{0}-{1}", StringHelper.ClearTurkishCharacter(request.Name), StringHelper.ClearTurkishCharacter(request.ApplicationName)), request.RefreshTime, () => _collection.GetService<ILookUpValueRepository>().GetValue(request));
            return response;
        }

        /// <summary>
        /// Gelen değer PostgreSQL üzerine kayıt edilir.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public bool SetValue(SetValueRequest request)
        {
            new SetValueControl().Validate(request);

            LookUpValue _model = new LookUpValue();
            _model.ApplicationName = request.ApplicationName;
            _model.IsActive = true;
            _model.Name = request.Name;
            _model.Value = request.Value;

            var response = _collection.GetService<ILookUpValueRepository>().Add(_model);

            _cache.Add(string.Format("{0}-{1}", StringHelper.ClearTurkishCharacter(request.Name), StringHelper.ClearTurkishCharacter(request.ApplicationName)), response.Value, request.RefreshTime);

            return true;
        }

        /// <summary>
        /// Örnek kullanım için yazılmıştır. Mail ve Log
        /// </summary>
        /// <returns></returns>
        public bool Example()
        {
            _logger.Add(new LogEntry(LogEventType.Information, "deneme mesajı", new Exception("dasdjahdjaw")));

            MailRequestDTO mailRequest = new MailRequestDTO();
            mailRequest.To = new List<MailAddress>() { new MailAddress("info@osmankurt.net") };
            mailRequest.Subject = "Deneme Konusu";
            mailRequest.Content = "Deneme İçeriği";
            _mail.Send(mailRequest);

            return true;
        }

        /// <summary>
        /// UnitTest için yazılmış method
        /// </summary>
        /// <param name="date"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string ToPrettyDate(DateTime date, CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return date.ToString("dd MMMMM yyyy", culture);
        }
    }
}
