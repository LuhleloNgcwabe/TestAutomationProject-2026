using System;
using System.Collections.Generic;
using System.Text;
using Serilog;

namespace OpenCart.Utility
{
    /// <summary>
    /// Centralize logging utilile
    /// </summary>
    public static class LogStep
    {
        /// <summary>
        /// Give more information about test progress
        /// </summary>
        /// <param name="message">Template string for the message, made up informative text with place holders for args</param>
        /// <param name="args">values to be used to complete placeholder in template string</param>
        public static void Info(string message, params object[] args)
        {
            Log.Information("STEP: " + message, args);
        }
        /// <summary>
        /// Used internal values, generated data, and low-level details that help debugging but aren’t important for normal reading.
        /// </summary>
        /// <param name="message">Template string for the message, made up informative text with place holders for args</param>
        /// <param name="args">values to be used to complete placeholder in template string</param>
        public static void Debug(string message, params object[] args)
        {
            Log.Debug("STEP DEBUG: " + message, args);
        }
        /// <summary>
        /// Warning → recoverable issues
        /// </summary>
        /// <param name="message">Template string for the message, made up informative text with place holders for args</param>
        /// <param name="args">values to be used to complete placeholder in template string</param>
        public static void Warn(string message, params object[] args)
        {
            Log.Warning("STEP WARN: " + message, args);
        }

        /// <summary>
        /// Error → failures (usually automatic in Run)
        /// </summary>
        /// <param name="message">Template string for the message, made up informative text with place holders for args</param>
        /// <param name="args">values to be used to complete placeholder in template string</param>
        public static void Error(string message, params object[] args)
        {
            Log.Error("STEP ERROR: " + message, args);
        }
        /// <summary>
        /// Fatal → framework/setup crash
        /// </summary>
        /// <param name="message">Template string for the message, made up informative text with place holders for args</param>
        /// <param name="args">values to be used to complete placeholder in template string</param>

        public static void Fatal(string message, params object[] args)
        {
            Log.Fatal("STEP FATAL: " + message, args);
        }
    }
}
