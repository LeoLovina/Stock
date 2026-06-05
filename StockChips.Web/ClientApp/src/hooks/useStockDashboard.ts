import { useQuery } from '@tanstack/react-query';
import { api } from '@/utils/api';
import type { StockChipDashboard, StockSearchResult } from '@/types/stocks';

export function useStockSearch(query: string) {
  return useQuery({
    queryKey: ['stock-search', query],
    enabled: query.trim().length > 0,
    queryFn: async () => {
      const response = await api.get<StockSearchResult[]>('/api/stocks/search', {
        params: { query }
      });

      return response.data;
    }
  });
}

export function useStockDashboard(stockId: string) {
  return useQuery({
    queryKey: ['stock-dashboard', stockId],
    enabled: stockId.trim().length > 0,
    queryFn: async () => {
      const response = await api.get<StockChipDashboard>(`/api/stocks/${stockId}/chips`);
      return response.data;
    }
  });
}
