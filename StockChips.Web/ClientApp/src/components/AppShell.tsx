import type { ReactNode } from 'react';

type AppShellProps = {
  children: ReactNode;
};

export function AppShell({ children }: AppShellProps) {
  return (
    <div className="page-shell">
      <header className="topbar">
        <div>
          <p className="eyebrow">FinMind 籌碼研究</p>
          <h1>台股籌碼儀表板</h1>
        </div>
        <span className="status-pill">TWSE + TPEx</span>
      </header>
      <main>{children}</main>
    </div>
  );
}
