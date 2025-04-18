import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "./App";
import NavBar from "./Navbar";  
import Dashboard from "./Dashboard";
import Kursy from "./Kursy";
import KursDetails from "./DetailsKurs";
import CreateKurs from "./CreateKurs";
import CreateZadanie from "./CreateZadanie";
import CreateOdpowiedz from "./CreateOdpowiedz";
import Zarzadzajuzytkownikami from "./ZarzadzajUzytkownikami";
import EditUser from "./EdytujUsers";
export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: 'navbar', element: <NavBar /> },
            { path: 'dashboard', element: <Dashboard />},
            { path: 'kursy', element: <Kursy/>},
            { path: 'details/:kursId', element: <KursDetails /> },
            { path: '/kurs/create', element: <CreateKurs /> },
            { path: 'kurs/:kursId/zadanie/create', element: < CreateZadanie /> },
            { path: `/kurs/:kursId/zadanie/:IdZadania/odpowiedz`, element: <CreateOdpowiedz /> },
            { path: '/users', element: <Zarzadzajuzytkownikami /> },
            { path: '/edytuj/:userId', element: <EditUser /> }
        ]
    }
];

export const router = createBrowserRouter(routes);
