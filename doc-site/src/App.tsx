import { createRouter, RouterProvider } from "@tanstack/react-router";
import "./App.css";
import { routeTree } from "./routeTree.gen";
import { type Service, ServiceProvider } from "./services";
import { themeService } from "./services/ThemeService";
import { MDXService } from "./services/MDXService";

const router = createRouter({ routeTree });

const services: Service[] = [themeService(), MDXService()];

function App() {
  return (
    <ServiceProvider services={services}>
      <RouterProvider router={router} />
    </ServiceProvider>
  );
}

export default App;
