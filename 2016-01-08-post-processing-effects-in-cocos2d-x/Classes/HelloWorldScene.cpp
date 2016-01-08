#include "HelloWorldScene.h"
/*****************************************
// Post-Processing Effects in Cocos2d-x //
#include "PostProcess.hpp"
*****************************************/
#include "PostProcess.hpp"

USING_NS_CC;

Scene* HelloWorld::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = HelloWorld::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Vec2 origin = Director::getInstance()->getVisibleOrigin();

/*****************************************
// Post-Processing Effects in Cocos2d-x //
+	m_gameLayer = Layer::create();
+	this->addChild(m_gameLayer, 0);
+
+	m_colorPostProcessLayer = PostProcess::create("shaders/generic.vert", "shaders/color_transition.frag");
+	m_colorPostProcessLayer->setAnchorPoint(Point::ZERO);
+	m_colorPostProcessLayer->setPosition(Point::ZERO);
+	this->addChild(m_colorPostProcessLayer, 1);
+
+	m_grayPostProcessLayer = PostProcess::create("shaders/generic.vert", "shaders/gray.frag");
+	m_grayPostProcessLayer->setAnchorPoint(Point::ZERO);
+	m_grayPostProcessLayer->setPosition(Point::ZERO);
+	this->addChild(m_grayPostProcessLayer, 2);
*****************************************/
	m_gameLayer = Layer::create();
	this->addChild(m_gameLayer, 0);

	m_colorPostProcessLayer = PostProcess::create("shaders/generic.vert", "shaders/color_transition.frag");
	m_colorPostProcessLayer->setAnchorPoint(Point::ZERO);
	m_colorPostProcessLayer->setPosition(Point::ZERO);
	this->addChild(m_colorPostProcessLayer, 1);

	m_grayPostProcessLayer = PostProcess::create("shaders/generic.vert", "shaders/gray.frag");
	m_grayPostProcessLayer->setAnchorPoint(Point::ZERO);
	m_grayPostProcessLayer->setPosition(Point::ZERO);
	this->addChild(m_grayPostProcessLayer, 2);

	/////////////////////////////
    // 2. add a menu item with "X" image, which is clicked to quit the program
    //    you may modify it.

    // add a "close" icon to exit the progress. it's an autorelease object
    auto closeItem = MenuItemImage::create(
                                           "CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(HelloWorld::menuCloseCallback, this));
    
	closeItem->setPosition(Vec2(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    // create menu, it's an autorelease object
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Vec2::ZERO);
/*****************************************
// Post-Processing Effects in Cocos2d-x //
-	this->addChild(menu, 1);
+	m_gameLayer->addChild(menu, 1);
*****************************************/
	m_gameLayer->addChild(menu, 1);

    /////////////////////////////
    // 3. add your codes below...

    // add a label shows "Hello World"
    // create and initialize a label
    
    auto label = Label::createWithTTF("Hello World", "fonts/Marker Felt.ttf", 24);
    
    // position the label on the center of the screen
    label->setPosition(Vec2(origin.x + visibleSize.width/2,
                            origin.y + visibleSize.height - label->getContentSize().height));

    // add the label as a child to this layer
/*****************************************
// Post-Processing Effects in Cocos2d-x //
-	this->addChild(label, 1);
+	m_gameLayer->addChild(label, 1);
*****************************************/
	m_gameLayer->addChild(label, 1);

    // add "HelloWorld" splash screen"
    auto sprite = Sprite::create("HelloWorld.png");

    // position the sprite on the center of the screen
    sprite->setPosition(Vec2(visibleSize.width/2 + origin.x, visibleSize.height/2 + origin.y));

    // add the sprite as a child to this layer
/*****************************************
// Post-Processing Effects in Cocos2d-x //
-	this->addChild(sprite, 0);
+	m_gameLayer->addChild(sprite, 0);
*****************************************/
	m_gameLayer->addChild(sprite, 0);

/*****************************************
// Post-Processing Effects in Cocos2d-x //
+	this->scheduleUpdate();
*****************************************/
	this->scheduleUpdate();
    
    return true;
}


void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}

/*****************************************
// Post-Processing Effects in Cocos2d-x //
+void HelloWorld::update(float delta)
+{
+	m_colorPostProcessLayer->draw(m_gameLayer);
+	m_grayPostProcessLayer->draw(m_colorPostProcessLayer);
+	//m_nextPostProcessLayer->draw(m_grayPostProcessLayer);
+	//m_anotherPostProcessLayer->draw(m_nextPostProcessLayer);
+	//...
+}
*****************************************/
void HelloWorld::update(float delta)
{
	m_colorPostProcessLayer->draw(m_gameLayer);
	m_grayPostProcessLayer->draw(m_colorPostProcessLayer);
	//m_nextPostProcessLayer->draw(m_grayPostProcessLayer);
	//m_anotherPostProcessLayer->draw(m_nextPostProcessLayer);
	//...
}
