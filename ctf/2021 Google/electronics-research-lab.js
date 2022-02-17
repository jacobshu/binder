/*
 * Challenge Download:
 * https://storage.googleapis.com/gctf-2021-attachments-project/eafc850054672b6e5242ffb8b2f3110760a20cabcca90a69c00c4f4c91912c2e43c5ea8e68ad529692da3aac7763f6301888b843c7ee5e94699e22c8ea94db5c
 *
 * From the Raspberry Pi Pico docs:
 * gpio_set_masks drives the GPIOs in the uint_32 mask high
 * gpio_clr_masks drives the GPIOs in the mask low;
 *
 */

class SolveBitMasks {
  constructor() {
    this.array = [];
    this.bits = 0b00000000;
  }

  set_mask(mask) {
    this.bits = this.bits | mask;
  }

  clr_mask(mask) {
    this.bits = this.bits & ~mask;
  }

  add_bits() {
    this.array.push(this.bits);
  }

  make_string() {
    return this.array.map(bits => String.fromCharCode(bits)).join('');
  }
}

let Solver = new SolveBitMasks();

Solver.set_mask(67);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(20);
Solver.clr_mask(3);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(16);
Solver.add_bits();

Solver.set_mask(57);
Solver.clr_mask(4);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(25);
Solver.add_bits();

Solver.set_mask(5);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(18);
Solver.clr_mask(65);
Solver.add_bits();

Solver.set_mask(1);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(64);
Solver.clr_mask(17);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(1);
Solver.clr_mask(6);
Solver.add_bits();

Solver.set_mask(18);
Solver.clr_mask(65);
Solver.add_bits();

Solver.set_mask(1);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(4);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(64);
Solver.clr_mask(16);
Solver.add_bits();

Solver.set_mask(16);
Solver.clr_mask(64);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(4);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(3);
Solver.add_bits();

Solver.set_mask(9);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(1);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(8);
Solver.add_bits();

Solver.set_mask(8);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(65);
Solver.clr_mask(24);
Solver.add_bits();

Solver.set_mask(22);
Solver.clr_mask(64);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(5);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(65);
Solver.clr_mask(16);
Solver.add_bits();

Solver.set_mask(22);
Solver.clr_mask(65);
Solver.add_bits();

Solver.set_mask(1);
Solver.clr_mask(6);
Solver.add_bits();

Solver.set_mask(4);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(66);
Solver.clr_mask(21);
Solver.add_bits();

Solver.set_mask(1);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(0);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(24);
Solver.clr_mask(65);
Solver.add_bits();

Solver.set_mask(67);
Solver.clr_mask(24);
Solver.add_bits();

Solver.set_mask(24);
Solver.clr_mask(67);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(8);
Solver.add_bits();

Solver.set_mask(65);
Solver.clr_mask(18);
Solver.add_bits();

Solver.set_mask(16);
Solver.clr_mask(64);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(0);
Solver.add_bits();

Solver.set_mask(68);
Solver.clr_mask(19);
Solver.add_bits();

Solver.set_mask(19);
Solver.clr_mask(64);
Solver.add_bits();

Solver.set_mask(72);
Solver.clr_mask(2);
Solver.add_bits();

Solver.set_mask(2);
Solver.clr_mask(117);
Solver.add_bits();
