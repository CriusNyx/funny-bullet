import "react";
import type { Service } from ".";
import { MDXProvider } from "@mdx-js/react";
import { HBar } from "../common/HBar";

export const MDXComponents = {
  h2: (props) => (
    <>
      <h2>{props.children}</h2>
      <HBar className="mb-5" />
    </>
  ),
} satisfies React.ComponentProps<typeof MDXProvider>["components"];

export const MDXService = (): Service => ({ children }) => {
  return (
    <MDXProvider
      components={MDXComponents}
    >
      {children}
    </MDXProvider>
  );
};
