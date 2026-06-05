export type StockSearchResult = {
  stockId: string;
  stockName: string;
  market: 'twse' | 'tpex' | string;
  industryCategory: string;
};

export type StockChipDashboard = {
  stockId: string;
  latestDataDate: string;
  institutionalInvestors: InstitutionalInvestorRow[];
  marginShort: MarginShortRow[];
  holdingShares: HoldingShareRow[];
  signals: string[];
};

export type InstitutionalInvestorRow = {
  date: string;
  name: string;
  buy: number;
  sell: number;
  buySell: number;
};

export type MarginShortRow = {
  date: string;
  marginBalance: number;
  shortBalance: number;
  marginChange: number;
  shortChange: number;
};

export type HoldingShareRow = {
  date: string;
  level: string;
  percent: number;
};
