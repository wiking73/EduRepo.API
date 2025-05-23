﻿import { RouteObject, createBrowserRouter } from "react-router-dom";
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
import Odpowiedzi from "./Odpowiedzi";
import UserProfile from "./UserProfile";
import OcenOdpowiedz from "./OcenOdpowiedz";
import EditKurs from "./EdytujKurs";
import ZgloszeniaDoKursu from "./ZgloszeniaDoKursu";
import UczestnicyKursu from "./UczestnicyKursu";
export const routes: RouteObject[] = [
    {
        path: "/",
        element: <App />,
        children: [
            { path: 'navbar', element: <NavBar /> },
            { path: 'dashboard', element: <Dashboard />},
            { path: 'kursy', element: <Kursy/>},
            { path: 'details/:id', element: <KursDetails /> },
            { path: '/kurs/create', element: <CreateKurs /> },
            { path: 'kurs/:id/zadanie/create', element: < CreateZadanie /> },
            { path: `/kurs/:id/zadanie/:IdZadania/odpowiedz`, element: <CreateOdpowiedz /> },
            { path: 'kurs/:id/zgloszenia', element: <ZgloszeniaDoKursu /> },
            { path: '/kurs/:id/uczestnicy', element: <UczestnicyKursu /> },
            { path: '/users', element: <Zarzadzajuzytkownikami /> },
            { path: '/edytuj/:id', element: <EditUser /> },
            { path: '/kurs/:id/zadanie/:IdZadania/odpowiedzi', element: <Odpowiedzi /> },
            { path: '/edit/:id/:IdZadania/:IdOdpowiedzi', element: <OcenOdpowiedz /> },
            { path: '/profile', element: <UserProfile /> },
            { path: '/edit/:id', element: <EditKurs />},
        ]
    }
];

export const router = createBrowserRouter(routes);
