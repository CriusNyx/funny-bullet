import { createFileRoute } from "@tanstack/react-router";
import { useMemo } from "react";
import _ from "lodash";
import { getDocInfo } from "../../docs";

export const Route = createFileRoute("/docs/$")({
  component: RouteComponent,
});

function RouteComponent() {
  const { _splat } = Route.useParams();
  const docInfo = useMemo(() => {
    return getDocInfo(_splat);
  }, [_splat]);
  if (docInfo) {
    return <docInfo.component />;
  }
}
