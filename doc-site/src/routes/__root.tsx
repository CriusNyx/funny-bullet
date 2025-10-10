import { createRootRoute, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";
import { Navigation } from "./__navigation";

const RootLayout = () => (
  <div className="flex flex-row w-full h-full overflow-hidden">
    <div className="flex flex-col bg-stone-800 min-w-50 gap-2">
      <Navigation />
    </div>
    <hr />
    <div className="flex flex-col page-gradiant grow overflow-y-scroll">
      <div className="mx-10 my-5 p-10 bg-neutral-950/70 rounded-2xl grow">
        <Outlet />
      </div>
    </div>
    <TanStackRouterDevtools />
  </div>
);

export const Route = createRootRoute({ component: RootLayout });
