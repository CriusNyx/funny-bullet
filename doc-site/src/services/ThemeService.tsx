import type { PropsWithChildren } from "react";
import type { Service } from ".";
import { ConfigProvider, theme } from "antd";

export const themeService =
  (): Service => ({ children }: PropsWithChildren) => {
    return (
      <ConfigProvider theme={{ algorithm: theme.darkAlgorithm }}>
        {children}
      </ConfigProvider>
    );
  };
