import Head from 'next/head';
import { StockDashboard } from '@/components/StockDashboard';

export default function HomePage() {
  return (
    <>
      <Head>
        <title>台股籌碼儀表板</title>
        <meta name="description" content="以 FinMind 資料建立的台股籌碼研究儀表板" />
      </Head>
      <StockDashboard />
    </>
  );
}
