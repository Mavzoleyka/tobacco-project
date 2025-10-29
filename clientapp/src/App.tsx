import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Catalog from './pages/Catalog';
import About from './pages/About';
import { Link } from 'react-router-dom';
import Home from './pages/Home';

function App() {
  return (
    <BrowserRouter>
    <nav>
        <Link to="/">Каталог</Link> | <Link to="/about">О магазине</Link>
    </nav>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/" element={<Catalog />} />
        {/* Потом добавишь другие страницы */}
         <Route path="/about" element={<About />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;