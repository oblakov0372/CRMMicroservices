import { useState } from "react";
import styles from "./CRMHeader.module.scss";
import { Link } from "react-router-dom";

type Menu = {
  title: string;
  description: string;
  link: string;
};

const CRMHeader = () => {
  const [activatedMenu, setActivatedMenu] = useState(0);
  const menus: Menu[] = [
    {
      title: "Messages",
      description: "Messages History",
      link: "/crm/messages",
    },
    {
      title: "Accounting",
      description: "Telegram Users",
      link: "/crm/accounting",
    },
    { title: "My Deals", description: "Leads & Deal", link: "/crm/deals" },
  ];
  return (
    <div className={styles.crm_header}>
      <ul className="flex items-center">
        {menus.map((menu: Menu, index: number) => (
          <li
            key={menu.title}
            onClick={() => setActivatedMenu(index)}
            className={
              activatedMenu === index
                ? `${styles.box} ${styles.active}`
                : `${styles.box}`
            }
          >
            <Link to={menu.link} className="block">
              <span className="text-2xl uppercase font-bold">{menu.title}</span>
              <span className="block text-lg font-medium">
                {menu.description}
              </span>
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CRMHeader;
