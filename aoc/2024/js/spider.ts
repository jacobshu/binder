// https://www.musora.com/login

import { chromium, FullConfig } from '@playwright/test';
import * as fs from 'fs'

export default async function globalSetup(config: FullConfig) {
  // (1) read the config
  const { storageState, headless } = config.projects[0].use;
  const baseURL = 'https://musora.com'

  // (2) instantiate
  const browser = await chromium.launch({ headless });
  const context = await browser.newContext();
  const page = await context.newPage();

  console.log(`\x1b[2m\tSign in started against '${baseURL}'\x1b[0m`);

  // (3) navigate to the login page
  await page.goto(baseURL);

  console.log(`\x1b[2m\tSign in as 'username'\x1b[0m`);

  // (4-5) fill in credentials and sign in
  await page.fill('input[name="email"]', '');
  await page.click('button[dusk=submit-button]')
  await page.fill('input[name="password"]', '');
  await page.click('button[dusk=submit-button]')

  console.log(`\x1b[2m\tSign in processed\x1b[0m`);
  const startingPage = ''
  let level = 6
  let lesson = 0
  let section = 'Theory'
  let sectionNumber = 1

  await page.goto(startingPage)

  let nextBtn: HTMLButtonElement = document.querySelector('div[dusk=next-lesson] a')!
  while (nextBtn) {
    let newSection = getSectionName()
    let lessonName = getLessonName()
    if (newSection === section) {
      lesson += 1
    } else {
      lesson = 1
      sectionNumber += 1
    }
    if (newSection !== section && section === 'Music') {
      level += 1
      sectionNumber = 0
    }
    section = newSection

    const dlName = `Level ${level}@${level}.${sectionNumber} ${section}@Lesson ${lesson} - ${lessonName}`
    await df(dlName)
    nextBtn.click()
  }

  // await df()
  // (6) persist the authentication state (local storage and cookies)
  await page.context().storageState({ path: storageState as string });

  // (7) close the browser
  await browser.close();
}

async function df(fileName: string) {
  let v: HTMLVideoElement = document.querySelector("#video-component-id")!
  let vsrc = v.src

  const link = document.createElement("a");
  link.style.display = "none";

  const response = await fetch(vsrc);
  const blob = await response.blob();
  link.href = URL.createObjectURL(blob);
  link.download = fileName

  document.body.appendChild(link);
  link.click();

  setTimeout(() => {
    URL.revokeObjectURL(link.href);
    link.parentNode!.removeChild(link);
  }, 0);
}

function getLessonName() {
  return document.querySelector('div[breadcrumb-last-level-title]')?.getAttribute('breadcrumb-last-level-title')!
}

function getSectionName() {
  let breadcrumbs: NodeListOf<HTMLAnchorElement> = document.querySelectorAll('main section input#sessionToken+div>div>div+div a')
  let cat = breadcrumbs[breadcrumbs.length - 1].innerText
  return `${cat[0]}${cat.slice(1).toLowerCase()}`

}

