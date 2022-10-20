using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SQLIndexManager_WPF.Infrastructure.Enums;
using SQLIndexManager_WPF.Infrastructure.Extensions;

namespace SQLIndexManager_WPF.Infrastructure.Settings
{
    [Serializable]
    public class Options
    {

        #region Fields
        private IndexOperation _skipOperation = IndexOperation.IGNORE;
        private IndexOperation _firstOperation = IndexOperation.REORGANIZE;
        private IndexOperation _secondOperation = IndexOperation.REBUILD;
        private int _firstThreshold = 15;
        private int _secondThreshold = 30;
        private int _minIndexSize = 6;
        private int _preDescribeSize = 128;
        private int _maxIndexSize = 8192;
        private int _connectionTimeout = 15;
        private int _commandTimeout = 120;
        private int _maxDop;
        private int _fillFactor;
        private int _sampleStatsPercent = 100;
        private int _statsIgnoreHours = 24;
        private int _statsIgnoreSampledPercent = 95;
        private int _maxDuration = 1;
        private DataCompression _dataCompression;
        private List<string> _includeSchemas = new List<string>();
        private List<string> _excludeSchemas = new List<string>();
        private List<string> _includeObject = new List<string>();
        private List<string> _excludeObject = new List<string>();
        #endregion

        #region Constructors
        public Options()
        {
            SortInTempDb = true;
            LobCompaction = true;
            ScanHeap = true;
            ScanClusteredIndex = true;
            ScanNonClusteredIndex = true;
            ScanClusteredColumnstore = true;
            ScanNonClusteredColumnstore = true;
            IgnorePermissions = true;
            IgnoreReadOnlyFL = true;
            ShowSettingsWhenConnectionChanged = true;
            StatsIgnoreHoursEnabled = false;
            StatsIgnoreSampledPercentEnabled = false;

            ScanMode = ScanMode.LIMITED;
            DataCompression = DataCompression.DEFAULT;
            NoRecompute = NoRecompute.DEFAULT;
            AbortAfterWait = AbortAfterWait.NONE;
        }
        #endregion

        #region Properties
        [XmlAttribute]
        public int ConnectionTimeout
        {
            get => _connectionTimeout;
            set => _connectionTimeout = value.IsBetween(15, 90) ? value : _connectionTimeout;
        }

        [XmlAttribute]
        public int CommandTimeout
        {
            get => _commandTimeout;
            set => _commandTimeout = value.IsBetween(0, 1800) ? value : _commandTimeout;
        }

        [XmlAttribute]
        public IndexOperation SkipOperation
        {
            get => _skipOperation;
            set => _skipOperation = CheckAllowedIndexOperation(value, _skipOperation, true);
        }

        [XmlAttribute]
        public IndexOperation FirstOperation
        {
            get => _firstOperation;
            set => _firstOperation = CheckAllowedIndexOperation(value, _firstOperation);
        }

        [XmlAttribute]
        public IndexOperation SecondOperation
        {
            get => _secondOperation;
            set => _secondOperation = CheckAllowedIndexOperation(value, _secondOperation);
        }

        [XmlAttribute]
        public int FirstThreshold
        {
            get => _firstThreshold;
            set => UpdateThreshold(value, _secondThreshold);
        }

        [XmlAttribute]
        public int SecondThreshold
        {
            get => _secondThreshold;
            set => UpdateThreshold(_firstThreshold, value);
        }

        [XmlAttribute]
        public int MaxDop
        {
            get => _maxDop;
            set => _maxDop = value.IsBetween(0, 64) ? value : _maxDop;
        }

        [XmlAttribute]
        public int FillFactor
        {
            get => _fillFactor;
            set => _fillFactor = value.IsBetween(0, 100) ? value : _fillFactor;
        }

        [XmlAttribute]
        public int SampleStatsPercent
        {
            get => _sampleStatsPercent;
            set => _sampleStatsPercent = value.IsBetween(1, 100) ? value : _sampleStatsPercent;
        }

        [XmlAttribute]
        public int StatsIgnoreHours
        {
            get => _statsIgnoreHours;
            set => _statsIgnoreHours = value.IsBetween(1, 100) ? value : _statsIgnoreHours;
        }

        [XmlAttribute]
        public bool StatsIgnoreHoursEnabled { get; set; }

        [XmlAttribute]
        public int StatsIgnoreSampledPercent
        {
            get => _statsIgnoreSampledPercent;
            set => _statsIgnoreSampledPercent = value.IsBetween(1, 99) ? value : _statsIgnoreSampledPercent;
        }

        [XmlAttribute]
        public bool StatsIgnoreSampledPercentEnabled;

        [XmlAttribute]
        public int MinIndexSize
        {
            get => _minIndexSize;
            set => UpdateSize(value, _preDescribeSize, _maxIndexSize);
        }

        [XmlAttribute]
        public int PreDescribeSize
        {
            get => _preDescribeSize;
            set => UpdateSize(_minIndexSize, value, _maxIndexSize);
        }

        [XmlAttribute]
        public int MaxIndexSize
        {
            get => _maxIndexSize;
            set => UpdateSize(_minIndexSize, _preDescribeSize, value);
        }

        [XmlAttribute]
        public bool ShowSettingsWhenConnectionChanged { get; set; }

        [XmlAttribute]
        public bool Online { get; set; }

        [XmlAttribute]
        public bool PadIndex { get; set; }

        [XmlAttribute]
        public bool SortInTempDb { get; set; }

        [XmlAttribute]
        public bool LobCompaction { get; set; }

        [XmlAttribute]
        public bool WaitAtLowPriority { get; set; }

        [XmlAttribute]
        public bool ScanHeap { get; set; }

        [XmlAttribute]
        public bool ScanClusteredIndex { get; set; }

        [XmlAttribute]
        public bool ScanNonClusteredIndex { get; set; }

        [XmlAttribute]
        public bool ScanClusteredColumnstore { get; set; }

        [XmlAttribute]
        public bool ScanNonClusteredColumnstore { get; set; }

        [XmlAttribute]
        public bool ScanMissingIndex { get; set; }

        [XmlAttribute]
        public bool IgnorePermissions { get; set; }

        [XmlAttribute]
        public bool IgnoreReadOnlyFL { get; set; }

        [XmlAttribute]
        public bool IgnoreHeapWithCompression { get; set; }

        [XmlAttribute]
        public bool ShowOnlyMore1000Rows { get; set; }

        [XmlElement]
        public List<string> IncludeSchemas
        {
            get => _includeSchemas;
            set => _includeSchemas = value.RemoveInvalidTokens();
        }

        [XmlElement]
        public List<string> ExcludeSchemas
        {
            get => _excludeSchemas;
            set => _excludeSchemas = value.RemoveInvalidTokens();
        }

        [XmlElement]
        public List<string> ExcludeObject
        {
            get => _excludeObject;
            set => _excludeObject = value.RemoveInvalidTokens();
        }

        [XmlElement]
        public List<string> IncludeObject
        {
            get => _includeObject;
            set => _includeObject = value.RemoveInvalidTokens();
        }

        [XmlAttribute]
        public int MaxDuration
        {
            get => _maxDuration;
            set => _maxDuration = value.IsBetween(1, 10) ? value : _maxDuration;
        }

        [XmlAttribute]
        public NoRecompute NoRecompute;

        [XmlAttribute]
        public DataCompression DataCompression
        {
            get => _dataCompression;
            set => _dataCompression = (value != DataCompression.COLUMNSTORE && value != DataCompression.COLUMNSTORE_ARCHIVE) ? value : _dataCompression;
        }

        [XmlAttribute]
        public AbortAfterWait AbortAfterWait { get; set; }

        [XmlAttribute]
        public ScanMode ScanMode { get; set; }
        #endregion

        #region Methods
        private static IndexOperation CheckAllowedIndexOperation(IndexOperation newValue, IndexOperation oldValue, bool isSkip = false)
        {
            return (newValue == IndexOperation.REBUILD
                 || newValue == IndexOperation.REORGANIZE
                 || newValue == IndexOperation.UPDATE_STATISTICS_FULL
                 || newValue == IndexOperation.UPDATE_STATISTICS_RESAMPLE
                 || newValue == IndexOperation.UPDATE_STATISTICS_SAMPLE
                 || (isSkip && newValue == IndexOperation.NO_ACTION)
                 || (isSkip && newValue == IndexOperation.IGNORE)
              ) ? newValue : oldValue;
        }

        private void UpdateThreshold(int first, int second)
        {
            _firstThreshold = first.IsBetween(1, 99) ? first : _firstThreshold;
            _secondThreshold = second.IsBetween(2, 100) ? second : _secondThreshold;

            if (_firstThreshold > _secondThreshold)
                _secondThreshold = _firstThreshold + 1;

            if (_firstThreshold == _secondThreshold)
            {
                if (_firstThreshold > 0)
                    _firstThreshold--;
                else
                    _secondThreshold++;
            }
        }

        private void UpdateSize(int min, int pre, int max)
        {
            _minIndexSize = min.IsBetween(0, 255) ? min : _minIndexSize;
            _preDescribeSize = min.IsBetween(_minIndexSize, 256) ? pre : _preDescribeSize;
            _maxIndexSize = max.IsBetween(512, 131072) ? max : _maxIndexSize;

            if (_minIndexSize > _preDescribeSize)
                _preDescribeSize = _minIndexSize + 1;

            if (_minIndexSize == _maxIndexSize)
            {
                if (_minIndexSize > 0)
                    _minIndexSize--;
                else
                    _maxIndexSize++;
            }
        } 
        #endregion

    }
}
