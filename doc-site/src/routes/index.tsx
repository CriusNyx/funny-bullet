import { createFileRoute } from "@tanstack/react-router";
import Home from "../docs/Home.mdx";

export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  return <Home />;
}
