using NTBlogWeb.Core.Logging;
using System.Data.Common;
//using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Z.EntityFramework.Extensions;

namespace NTBlogWeb.Core.Interceptor
{
    public class EfInterceptorLogging : Z.EntityFramework.Extensions.DbCommandInterceptor
    {
        private ILogger _logger = new Logger();
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void ScalarExecuting(DbCommand command,
            DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.EventData != null)
            {
                _logger.Error(interceptionContext.EventData.ToString(), "Error executing command: {0}",
                    command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "EfInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}: ",
                    command.CommandText);
            }

            base.ScalarExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.EventData != null)
            {
                _logger.Error(interceptionContext.EventData.ToString(), "Error executing command: {0}",
                    command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "EfInterceptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}: ",
                    command.CommandText);
            }

            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command,
            DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command,
            DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.EventData != null)
            {
                _logger.Error(interceptionContext.EventData.ToString(), "Error executing command: {0}",
                    command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "EfInterceptor.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ",
                    command.CommandText);
            }

            base.ReaderExecuted(command, interceptionContext);
        }
    }
}