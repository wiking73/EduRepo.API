import logo from './logo.svg';
import { Outlet } from 'react-router-dom';
import NavBar from './Components/Navbar.tsx';
import 'semantic-ui-css/semantic.min.css';

function App() {
    return (
        <>
            <NavBar />
            <div style={{ marginTop: '10px' }}>
                <Outlet />
            </div>
        </>
    );
}

export default App;
