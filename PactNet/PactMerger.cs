﻿using System.Collections.Generic;
using System.Linq;
using PactNet.Models;
using PactNet.Wrappers;

namespace PactNet
{
	internal class PactMerger : IPactMerger
	{
		private readonly IFileWrapper _fileWrapper;
		private readonly string _pactDir;

		public PactMerger(string pactDir, IFileWrapper fileWrapper)
		{
			_pactDir = pactDir;
			_fileWrapper = fileWrapper;
		}

		public void DeleteUnexpectedInteractions(IEnumerable<Interaction> interactions, string consumer, string provider)
		{
			var pactFilePath = GetPactFilePath(_pactDir, consumer, provider);
			if (!_fileWrapper.Exists(pactFilePath)) return;

			if (interactions == null || !interactions.Any())
			{
				_fileWrapper.Delete(pactFilePath);
				return;
			}


		}

		private static string GetPactFilePath(string pactDir, string consumer, string provider)
		{
			var filePath = pactDir + @"\" + consumer.Replace(" ", "_") + "_" + provider.Replace(" ", "_") + ".txt";
			return filePath;
		}
	}
}