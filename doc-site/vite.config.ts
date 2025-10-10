import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";
import tanstackRouter from "@tanstack/router-plugin/vite";
import mdx from "@mdx-js/rollup";
import mdxRemark from "remark-mdx";
import remarkGFM from "remark-gfm";

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    tanstackRouter({ target: "react", autoCodeSplitting: true }),
    react(),
    mdx({
      providerImportSource: "@mdx-js/react",
      remarkPlugins: [mdxRemark, remarkGFM],
    }),
    tailwindcss(),
  ],
});
