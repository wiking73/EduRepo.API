import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "./App";
import NavBar from "./Navbar";  


export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: 'navbar', element: <NavBar /> },
           



        ]
    }
];

export const router = createBrowserRouter(routes);
