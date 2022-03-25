import { Fighter, Ranger, Wizard, SwordBehavior } from "../ch1";

describe("Strategy Pattern", () => {
  it("should encapsulate behaviors into interfaces", () => {
    let figherHero = new Fighter("Lan");
    Fighter.setWeapon(new SwordBehavior());
    console.log(Fighter);
  });
});
