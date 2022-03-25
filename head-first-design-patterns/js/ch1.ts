export class World {
  characters = 0;
  incrementCharacters() { this.characters += 1}
}

export class Character {
  constructor(world: World) {
    world.incrementCharacters();
    this.id = world.characters;
    this.name = null;
    this.weapon = null;
    return this;
  }
  id;
  name: string | null;
  weapon: WeaponBehavior | null;
  setWeapon(w: WeaponBehavior): Character {
    this.weapon = w;
    return this;
  }
}

export class Fighter extends Character {
  proficiency = [SwordBehavior, AxeBehavior]
  constructor(name: string) {
    super();
    this.name = name;
  }
}

export class Ranger extends Character {
  proficiency = [SwordBehavior, BowBehavior]
  constructor(name: string) {
    super();
    this.name = name;
  }
}

export class Wizard extends Character {
  proficiency = [StaffBehavior]
  constructor(name: string) {
    super();
    this.name = name;
  }
}

export interface WeaponBehavior {
  range: WeaponRange[];
  damageType: DamageType[];
  baseDamage: number;
  damageVariance: number;
  useWeapon(): { damageType: DamageType[]; damage: number, msg: string };
}

export class SwordBehavior implements WeaponBehavior {
  damageType = [DamageType.Cut, DamageType.Poke];
  baseDamage = 8;
  range = [WeaponRange.Melee];
  damageVariance = 10;
  useWeapon() {
    return {
      msg: 'Slash!',
      damageType: this.damageType,
      damage: this.baseDamage + Math.floor(Math.random() * this.damageVariance),
    };
  }
}

export class AxeBehavior implements WeaponBehavior {
  damageType = [DamageType.Cut, DamageType.Crush];
  baseDamage = 6;
  range = [WeaponRange.Melee];
  damageVariance = 15;
  useWeapon() {
    return {
      msg: 'Chop!',
      damageType: this.damageType,
      damage: this.baseDamage + Math.floor(Math.random() * this.damageVariance),
    };
  }
}

export class BowBehavior implements WeaponBehavior {
  damageType = [DamageType.Poke];
  baseDamage = 10;
  range = [WeaponRange.Ranged];
  damageVariance = 6;
  useWeapon() {
    return {
      msg: 'Shoot!',
      damageType: this.damageType,
      damage: this.baseDamage + Math.floor(Math.random() * this.damageVariance),
    };
  }
}

export class StaffBehavior implements WeaponBehavior {
  damageType = [DamageType.Crush, DamageType.Magic];
  baseDamage = 4;
  range = [WeaponRange.Ranged, WeaponRange.Melee];
  damageVariance = 25;
  useWeapon() {
    return {
      msg: 'Zap!',
      damageType: this.damageType,
      damage: this.baseDamage + Math.floor(Math.random() * this.damageVariance),
    };
  }
}

export enum DamageType {
  Cut = 'CUT',
  Poke = 'POKE',
  Crush = 'CRUSH',
  Magic = 'MAGIC',
}

export enum WeaponRange {
  Melee = "MELEE",
  Ranged = "RANGED",
}
