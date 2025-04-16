import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "./App";
import NavBar from "./Navbar";  
import Dashboard from "./Dashboard";
import Kursy from "./Kursy";
import KursDetails from "./DetailsKurs";
import CreateKurs from "./CreateKurs";
export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: 'navbar', element: <NavBar /> },
            { path: 'dashboard', element: <Dashboard />},
            { path: 'kursy', element: <Kursy/>},
            { path: 'details/:id', element: <KursDetails /> },
            { path: '/kurs/create', element: <CreateKurs /> }
        ]
    }
];

export const router = createBrowserRouter(routes);
