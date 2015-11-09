﻿/*
 * "Simple SQL Exec"
 * Copyright (c) 2015 Sql Quantum Leap. All rights reserved.
 * 
 */
using System;
using System.Data.SqlClient; // ApplicationIntent enum


namespace SimpleSqlExec
{
	internal class InputParameters
	{
        /* SQLCMD utility ( https://msdn.microsoft.com/en-us/library/ms162773.aspx )
         * SqlConnection.ConnectionString ( https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnection.connectionstring.aspx )
         * SqlCommand.CommandTimeout ( https://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlcommand.commandtimeout.aspx )
         * 
         * -U "User ID"
         * -P "Password"
         * -S "Server"
         * -d "Database name"
         * -H "Workstation name"
         * (-E trusted connection -- assumed true if -U and -P are not present)
         * -Q "Query"
         * -l "Login (i.e. connection) timeout"
         * -t "Query (i.e. command) timeout"
         * -K "Application intent"
         * -N Encrypt Connection
         * -C Trust Server Certificate
         * -M MultiSubnet Failover
         * -o "Output file"
         * -s "Column separator"
         * 
         * -an "Application name"
         * -cs "Connection string"
         * -ra "Rows Affected file path {or environment variable name?}"
         * -mf "Messages File"
         * -ef "Error File"
         * -? / -help  Display usage
         */

        private string _UserID = String.Empty;
        internal string UserID
		{
			get
			{
                return this._UserID;
			}
		}

        private string _Password = String.Empty;
        internal string Password
        {
            get
            {
                return this._Password;
            }
        }

        private string _Server = String.Empty;
        internal string Server
        {
            get
            {
                return this._Server;
            }
        }

        private string _DatabaseName = String.Empty;
        internal string DatabaseName
        {
            get
            {
                return this._DatabaseName;
            }
        }

        private string _WorkstationName = String.Empty;
        internal string WorkstationName
        {
            get
            {
                return this._WorkstationName;
            }
        }

        private string _Query = String.Empty;
        internal string Query
        {
            get
            {
                return this._Query;
            }
        }

        private int _LoginTimeout = 15; // .NET SqlConnection default
        internal int LoginTimeout
        {
            get
            {
                return this._LoginTimeout;
            }
        }

        private int _QueryTimeout = 30; // .NET SqlCommand default
        internal int QueryTimeout
        {
            get
            {
                return this._QueryTimeout;
            }
        }

        private ApplicationIntent _AppIntent = ApplicationIntent.ReadWrite; // .NET SqlConnection default
        internal ApplicationIntent AppIntent
        {
            get
            {
                return this._AppIntent;
            }
        }

        private bool _EncryptConnection = false; // .NET SqlConnection default
        internal bool EncryptConnection
        {
            get
            {
                return this._EncryptConnection;
            }
        }

        private bool _TrustServerCertificate = false; // .NET SqlConnection default
        internal bool TrustServerCertificate
        {
            get
            {
                return this._TrustServerCertificate;
            }
        }

        private bool _MultiSubnetFailover = false; // .NET SqlConnection default
        internal bool MultiSubnetFailover
        {
            get
            {
                return this._MultiSubnetFailover;
            }
        }

        private string _OutputFile = String.Empty;
        internal string OutputFile
        {
            get
            {
                return this._OutputFile;
            }
        }

        private string _ColumnSeparator = String.Empty;
        internal string ColumnSeparator
        {
            get
            {
                return this._ColumnSeparator;
            }
        }

        private string _ApplicationName = "Simple SQL Exec";
        internal string ApplicationName
        {
            get
            {
                return this._ApplicationName;
            }
        }

        private string _AttachDBFilename = String.Empty;
        internal string AttachDBFilename
        {
            get
            {
                return this._AttachDBFilename;
            }
        }

        private string _ConnectionString = String.Empty;
        internal string ConnectionString
        {
            get
            {
                return this._ConnectionString;
            }
        }

        private string _RowsAffectedDestination = String.Empty;
        internal string RowsAffectedDestination
        {
            get
            {
                return this._RowsAffectedDestination;
            }
        }

        private string _MessagesFile = String.Empty;
        internal string MessagesFile
        {
            get
            {
                return this._MessagesFile;
            }
        }

        private string _ErrorFile = String.Empty;
        internal string ErrorFile
        {
            get
            {
                return this._ErrorFile;
            }
        }

		private bool _DisplayUsage = false;
		internal bool DisplayUsage
		{
			get
			{
				return this._DisplayUsage;
			}
		}


        public InputParameters(string[] args)
		{
            if (args.Length == 0)
            {
                _DisplayUsage = true;

                return;
            }

			for (int _Index = 0; _Index < args.Length; _Index++)
			{
				switch (args[_Index])
				{
					case "-U":
					case "/U":
						this._UserID = args[++_Index];
						break;
					case "-P":
					case "/P":
						this._Password = args[++_Index];
						break;
					case "-S":
					case "/S":
						this._Server = args[++_Index];
						break;
                    case "-d":
                    case "/d":
                        this._DatabaseName = args[++_Index];
                        break;
                    case "-H":
					case "/H":
						this._WorkstationName = args[++_Index];
						break;
					case "-Q":
					case "/Q":
						this._Query = args[++_Index];
						break;
					case "-l":
					case "/l":
						Int32.TryParse(args[++_Index], out this._LoginTimeout);
                        if (this._LoginTimeout < 0)
                        {
                            throw new ArgumentException(String.Concat("Invalid Connect / Login Timeout value ",
                                _LoginTimeout, "; the value must be >= 0."), "-l");
                        }
						break;
					case "-t":
					case "/t":
                        Int32.TryParse(args[++_Index], out this._QueryTimeout);
                        if (this._QueryTimeout < 0)
                        {
                            throw new ArgumentException(String.Concat("Invalid Query / Command Timeout value ",
                                _QueryTimeout, "; the value must be >= 0."), "-t");
                        }
                        break;
                    case "-K":
                    case "/K":
                        Enum.TryParse<ApplicationIntent>(args[++_Index], out this._AppIntent);
						break;
                    case "-N":
                    case "/N":
                        this._EncryptConnection = true;
                        break;
                    case "-C":
                    case "/C":
                        this._TrustServerCertificate = true;
                        break;
                    case "-M":
                    case "/M":
                        this._MultiSubnetFailover = true;
                        break;
					case "-o":
					case "/o":
						this._OutputFile = args[++_Index];
						break;
                    case "-s":
                    case "/s":
                        this._ColumnSeparator = args[++_Index];
                        break;

                    case "-an":
                    case "/an":
                        this._ApplicationName = args[++_Index];
                        break;
                    case "-af":
                    case "/af":
                        this._AttachDBFilename = args[++_Index];
                        break;
                    case "-cs":
                    case "/cs":
                        this._ConnectionString = args[++_Index];
                        break;
                    case "-ra":
                    case "/ra":
                        this._RowsAffectedDestination = args[++_Index];
                        break;
                    case "-mf":
                    case "/mf":
                        this._MessagesFile = args[++_Index];
                        break;
                    case "-ef":
                    case "/ef":
                        this._ErrorFile = args[++_Index];
                        break;
                    case "-help":
					case "-?":
					case "/help":
					case "/?":
						this._DisplayUsage = true;
						break;
					default:
						throw new ArgumentException("Invalid parameter specified.", args[_Index]);
				}
			}
		}


	}
}