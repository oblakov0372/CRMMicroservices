import React from "react";
import styles from "./TelegramUserSections.module.scss";
const TelegramUserSections: React.FC<{
  activeSection: number;
  setActiveSection: (section: number) => void;
}> = ({ activeSection, setActiveSection }) => {
  const telegramUserSections = ["Message History", "Deals"];

  return (
    <ul className="mb-5 flex">
      {telegramUserSections.map((section, index) => (
        <li
          className={
            activeSection === index
              ? `${styles.section} ${styles.active}`
              : `${styles.section}`
          }
          onClick={() => setActiveSection(index)}
          key={section}
        >
          {section}
        </li>
      ))}
    </ul>
  );
};

export default TelegramUserSections;
