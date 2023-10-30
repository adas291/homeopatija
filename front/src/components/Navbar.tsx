import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <>
      <nav className="navbar">
        <Link to="vaistai">Vaistai</Link>
        <Link to="ligos">Ligos</Link>
        <Link to="bureja">Būrėja</Link>
      </nav>
    </>
  )
}