import { Link } from "@tanstack/react-router";
import { HBar } from "../common/HBar";
import { type DocInfo, type DocRoutes, docRoutes, isDoc } from "../docs";
import _ from "lodash";
import type { PropsWithChildren } from "react";

export function Navigation() {
  return (
    <div className="flex flex-col gap-2 pt-2">
      <NavLink to="/">
        Home
      </NavLink>
      <HBar />
      {_.entries(docRoutes).map(([path, info]) => (
        <DocRouteOrDoc path={`/docs/${path}`} name={path} value={info} />
      ))}
    </div>
  );
}

interface DocRouteOrDocProps {
  path: string;
  name: string;
  value: DocRoutes | DocInfo;
}

function DocRouteOrDoc(props: DocRouteOrDocProps) {
  if (isDoc(props.value)) {
    return <NavLink to={props.path}>{props.value.name}</NavLink>;
  } else {
    return (
      <>
        <h3>{props.name}</h3>
      </>
    );
  }
}

interface NavLinkProps extends PropsWithChildren {
  to: string;
}

function NavLink(props: NavLinkProps) {
  return (
    <div className="px-5">
      <Link className="[&.active]:font-bold" to={props.to}>
        {props.children}
      </Link>
    </div>
  );
}
